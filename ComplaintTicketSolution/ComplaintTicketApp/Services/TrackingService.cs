using System;
using System.Collections.Generic;
using System.Linq;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;
using ComplaintTicketApp.Repositories;

namespace ComplaintTicketApp.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IRepository<int, Tracking> _trackingRepository;

        public TrackingService(IRepository<int, Tracking> trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }

        public TrackingDTO AddTracking(TrackingDTO trackingDTO)
        {
            var trackingEntity = new Tracking
            {
                ComplaintId = trackingDTO.ComplaintId,
                UpdateDate = trackingDTO.UpdateDate,
                Status = trackingDTO.Status
                // add other properties as needed
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
                    // map other properties as needed
                };

                return addedTrackingDTO;
            }

            return null;
        }

        public TrackingDTO UpdateTrackingStatus(int trackingId, string status)
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
                        // map other properties as needed
                    };

                    return updatedTrackingDTO;
                }
            }

            return null;
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
                    // map other properties as needed
                };

                return trackingDTO;
            }

            return null;
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
                        // map other properties as needed
                    }).ToList();

                return trackingDTOs;
            }

            return null;
        }

        public bool RemoveTracking(int trackingId)
        {
            var removedTracking = _trackingRepository.Delete(trackingId);

            return removedTracking != null;
        }
    }
}
