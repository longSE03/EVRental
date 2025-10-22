using AutoMapper;
using AutoMapper.QueryableExtensions;
using EVRenter_Data.Entities;
using EVRenter_Repository.UnitOfWork;
using EVRenter_Service.RequestModel;
using EVRenter_Service.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Service.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleResponseModel>> GetAllVehicle();
        Task<VehicleResponseModel?> GetVehicleByIdAsync(int id);
        Task<VehicleResponseModel> CreateVehicleAsync(VehicleRequestModel request);
        Task<VehicleResponseModel?> UpdateVehicleAsync(int id, VehicleUpdateRequest request);
        Task<bool> DeleteVehicleAsync(int id);
    }
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleResponseModel>> GetAllVehicle()
        {
            return await _unitOfWork.Repository<Vehicle>()
                .GetQueryable()
                .Where(x => !x.IsDelete)
                .ProjectTo<VehicleResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<VehicleResponseModel?> GetVehicleByIdAsync(int id)
        {
            // Get the user with basic information
            var vehicle = await _unitOfWork.Repository<Vehicle>().AsQueryable()
                .Where(u => u.Id == id)
                .ProjectTo<VehicleResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return vehicle;
        }


        public async Task<VehicleResponseModel> CreateVehicleAsync(VehicleRequestModel request)
        {
            if (request == null)
                throw new ArgumentException("Invalid request data.");

            var model = await _unitOfWork.Repository<Model>().AsQueryable()
                .Where(u => u.Id == request.ModelID)
                .FirstOrDefaultAsync();
            if (model == null)
            {
                throw new Exception("Model not found");
            }

            var station = await _unitOfWork.Repository<Station>().AsQueryable()
                .Where(u => u.Id == request.StationID)
                .FirstOrDefaultAsync();
            if (station == null)
            {
                throw new Exception("Station not found");
            }

            var existingVehicle = await _unitOfWork.Repository<Vehicle>().FindAsync(u => u.PlateNumber == request.PlateNumber);
            if (existingVehicle != null)
            {
                throw new InvalidOperationException("Plate Number already exists.");
            }

            var vehicle = _mapper.Map<Vehicle>(request);

            await _unitOfWork.Repository<Vehicle>().InsertAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            var createdVehicle = await _unitOfWork.Repository<Vehicle>()
                .AsQueryable()
                .Where(x => x.Id == vehicle.Id)
                .ProjectTo<VehicleResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (createdVehicle == null)
            {
                throw new Exception("Failed to retrieve created Vehicle.");
            }

            return createdVehicle;
        }

        public async Task<VehicleResponseModel?> UpdateVehicleAsync(int id, VehicleUpdateRequest request)
        {
            var existingVehicle = await _unitOfWork.Repository<Vehicle>()
                .AsQueryable()
                .Where(s => s.Id == id && !s.IsDelete)
                .FirstOrDefaultAsync();

            if (existingVehicle == null) return null;
            //if (existingVehicle.ModelID == null)
            //{
            //    var existingModel = await _unitOfWork.Repository<Model>()
            //    .AsQueryable()
            //    .Where(s => s.Id == request. && !s.IsDelete)
            //    .FirstOrDefaultAsync();
            //}

            // Kiểm tra xem có bất kỳ trường nào được cập nhật không
            bool hasUpdates = false;

            // Cập nhật từng trường nếu có giá trị mới
            if (!string.IsNullOrEmpty(request.PlateNumber))
            {
                existingVehicle.PlateNumber = request.PlateNumber;
                hasUpdates = true;
            }
            if (request.ModelID.HasValue)
            {
                var model = await _unitOfWork.Repository<Model>().AsQueryable()
                .Where(u => u.Id == request.ModelID.Value)
                .FirstOrDefaultAsync();
                if (model == null)
                {
                    throw new Exception("Model not found");
                }

                existingVehicle.ModelID = request.ModelID.Value;
                hasUpdates = true;
            }

            if (request.StationID.HasValue)
            {
                var station = await _unitOfWork.Repository<Station>().AsQueryable()
                    .Where(u => u.Id == request.StationID.Value)
                    .FirstOrDefaultAsync();
                if (station == null)
                {
                    throw new Exception("Station not found");
                }

                existingVehicle.StationID = request.StationID.Value;
                hasUpdates = true;
            }

            if (request.Status.HasValue)
            {
                existingVehicle.Status = request.Status.Value;
                hasUpdates = true;
            }

            if (hasUpdates)
            {
                await _unitOfWork.Repository<Vehicle>().Update(existingVehicle, id);
                await _unitOfWork.SaveChangesAsync();
            }

            return _mapper.Map<VehicleResponseModel>(existingVehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var user = await _unitOfWork.Repository<Vehicle>().GetById(id);
            if (user == null) return false;

            user.IsDelete = true;
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
