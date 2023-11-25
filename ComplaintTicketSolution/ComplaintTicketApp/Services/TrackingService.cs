using System;
using System.Collections.Generic;
using System.Linq;
using ComplaintTicketApp.Exceptions;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;
using ComplaintTicketApp.Repositories;

namespace ComplaintTicketApp.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IRepository<int, Tracking> _trackingRepository;
        private readonly IRepository<int, Complaint> _complaintRepository;

        public TrackingService(
            IRepository<int, Tracking> trackingRepository,
            IRepository<int, Complaint> complaintRepository)
        {
            _trackingRepository = trackingRepository;
            _complaintRepository = complaintRepository;
        }

        public TrackingDTO AddTracking(TrackingDTO trackingDTO)
        {
            try
            {
                var trackingEntity = new Tracking
                {
                    ComplaintId = trackingDTO.ComplaintId,
                    UpdateDate = trackingDTO.UpdateDate,
                    Status = trackingDTO.Status
                };

                var addedTracking = _trackingRepository.Add(trackingEntity);

                if (addedTracking != null)
                {
                    var addedTrackingDTO = new TrackingDTO
                    {
                        TrackingId = addedTracking.TrackingId,
                        ComplaintId = addedTracking.ComplaintId,
                        UpdateDate = addedTracking.UpdateDate,
                        Status = addedTracking.Status
                    };

                    return addedTrackingDTO;
                }

                throw new TrackingAddException();
            }
            catch (Exception)
            {
                throw new TrackingOperationException();
            }
        }

        public TrackingDTO UpdateTrackingStatus(int trackingId, string status)
        {
            try
            {
                var trackingEntity = _trackingRepository.GetById(trackingId);

                if (trackingEntity != null)
                {
                    trackingEntity.Status = status;
                    trackingEntity.UpdateDate = DateTime.Now;

                    var updatedTracking = _trackingRepository.Update(trackingEntity);

                    if (updatedTracking != null)
                    {
                        var updatedTrackingDTO = new TrackingDTO
                        {
                            TrackingId = updatedTracking.TrackingId,
                            ComplaintId = updatedTracking.ComplaintId,
                            UpdateDate = updatedTracking.UpdateDate,
                            Status = updatedTracking.Status
                        };

                        return updatedTrackingDTO;
                    }

                    throw new TrackingUpdateException();
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                throw new TrackingOperationException();
            }
        }

        public TrackingDTO GetTrackingById(int trackingId)
        {
            var trackingEntity = _trackingRepository.GetById(trackingId);

            if (trackingEntity != null)
            {
                var trackingDTO = new TrackingDTO
                {
                    TrackingId = trackingEntity.TrackingId,
                    ComplaintId = trackingEntity.ComplaintId,
                    UpdateDate = trackingEntity.UpdateDate,
                    Status = trackingEntity.Status
                };

                return trackingDTO;
            }

            throw new TrackingNotFoundException();
        }

        public IList<TrackingDTO> GetAllTrackings()
        {
            var allTrackings = _trackingRepository.GetAll();

            if (allTrackings != null && allTrackings.Any())
            {
                var trackingDTOs = allTrackings
                    .Select(trackingEntity => new TrackingDTO
                    {
                        TrackingId = trackingEntity.TrackingId,
                        ComplaintId = trackingEntity.ComplaintId,
                        UpdateDate = trackingEntity.UpdateDate,
                        Status = trackingEntity.Status
                    }).ToList();

                return trackingDTOs;
            }

            throw new TrackingNotFoundException();
        }

        public bool RemoveTracking(int trackingId)
        {
            var removedTracking = _trackingRepository.Delete(trackingId);

            if (removedTracking != null)
            {
                return true;
            }

            throw new TrackingNotFoundException();
        }

        public TrackingDTO GetTrackingByComplaintId(int complaintId)
        {
            var trackingEntity = _trackingRepository.GetAll()
                .FirstOrDefault(tracking => tracking.ComplaintId == complaintId);

            if (trackingEntity != null)
            {
                var trackingDTO = new TrackingDTO
                {
                    TrackingId = trackingEntity.TrackingId,
                    ComplaintId = trackingEntity.ComplaintId,
                    UpdateDate = trackingEntity.UpdateDate,
                    Status = trackingEntity.Status
                };

                return trackingDTO;
            }

            throw new TrackingNotFoundException();
        }

        public TrackingDTO UpdateTrackingStatusByComplaintId(int complaintId, string status)
        {
            var trackingEntity = _trackingRepository.GetAll()
                .FirstOrDefault(tracking => tracking.ComplaintId == complaintId);

            if (trackingEntity != null)
            {
                trackingEntity.Status = status;
                trackingEntity.UpdateDate = DateTime.Now;

                var updatedTracking = _trackingRepository.Update(trackingEntity);

                if (updatedTracking != null)
                {
                    var updatedTrackingDTO = new TrackingDTO
                    {
                        TrackingId = updatedTracking.TrackingId,
                        ComplaintId = updatedTracking.ComplaintId,
                        UpdateDate = updatedTracking.UpdateDate,
                        Status = updatedTracking.Status
                    };

                    return updatedTrackingDTO;
                }

                throw new TrackingUpdateException();
            }

            throw new ComplaintNotFoundException();
        }
    }
}
