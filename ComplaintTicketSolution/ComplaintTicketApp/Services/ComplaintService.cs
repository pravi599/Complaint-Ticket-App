using System;
using System.Collections.Generic;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IRepository<int, Complaint> _complaintRepository;
        private readonly IRepository<int, Priority> _priorityRepository;
        private readonly IRepository<int, Tracking> _trackingRepository;
        private readonly IRepository<int, Organization> _organizationRepository;

        public ComplaintService(
            IRepository<int, Complaint> complaintRepository,
            IRepository<int, Priority> priorityRepository,
            IRepository<int, Tracking> trackingRepository,
            IRepository<int, Organization> organizationRepository)
        {
            _complaintRepository = complaintRepository;
            _priorityRepository = priorityRepository;
            _trackingRepository = trackingRepository;
            _organizationRepository = organizationRepository;
        }

        public bool Add(ComplaintDTO complaintDTO)
        {
            try
            {
                var organization = _organizationRepository.GetById(complaintDTO.OrganizationId);

                if (organization != null)
                {
                    var complaint = new Complaint
                    {
                        OrganizationId = complaintDTO.OrganizationId,
                        OrganizationName = complaintDTO.OrganizationName,
                        Category = complaintDTO.Category,
                        Description = complaintDTO.Description,
                        Username = complaintDTO.Username,
                        FilePath = complaintDTO.FilePath
                    };

                    _complaintRepository.Add(complaint);

                    var priority = new Priority
                    {
                        Name = "Low",
                        EscalationThreshold = 7,
                        Complaint = complaint
                    };
                    var tracking = new Tracking
                    {
                        Status = "Open",
                        UpdateDate = DateTime.Now,
                        Complaint = complaint
                    };
                    //complaint.Priority = priority; // Establish the relationship
                    //complaint.Tracking = tracking; // Establish the relationship
                    _priorityRepository.Add(priority);
                    _trackingRepository.Add(tracking);
                    return true;
                }
                else
                {
                    // Organization not found, handle accordingly
                    // (e.g., throw an exception, log, etc.)
                    Console.WriteLine("Organization not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for better error handling
                Console.WriteLine($"Error adding complaint: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<ComplaintDTO> GetAllComplaints()
        {
            try
            {
                var complaints = _complaintRepository.GetAll();

                var complaintDTOs = complaints
                    .Select(complaint => new ComplaintDTO
                    {
                        ComplaintId = complaint.ComplaintId,
                        Category = complaint.Category,
                        Description = complaint.Description,
                        OrganizationId = complaint.OrganizationId,
                        OrganizationName = complaint.OrganizationName,
                        Username = complaint.Username,
                        FilePath = complaint.FilePath
                    }).ToList();

                return complaintDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all complaints: {ex.Message}");
                return null;
            }
        }

        public ComplaintDTO GetComplaintById(int complaintId)
        {
            try
            {
                var complaint = _complaintRepository.GetById(complaintId);

                if (complaint != null)
                {
                    var complaintDTO = new ComplaintDTO
                    {
                        ComplaintId = complaint.ComplaintId,
                        Category = complaint.Category,
                        Description = complaint.Description,
                        OrganizationId = complaint.OrganizationId,
                        OrganizationName = complaint.OrganizationName,
                        Username = complaint.Username,
                        FilePath = complaint.FilePath
                    };
                    return complaintDTO;
                }
                else
                {
                    Console.WriteLine("Complaint not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting complaint by ID: {ex.Message}");
                return null;
            }
        }

        public bool Remove(int complaintId)
        {
            try
            {
                var complaint = _complaintRepository.Delete(complaintId);
                if (complaint != null)
                {

                    Console.WriteLine("Complaint and associated records deleted successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("Complaint not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting complaint: {ex.Message}");
                return false;
            }
        }

        public ComplaintDTO Update(ComplaintDTO complaintDTO)
        {
            try
            {
                // Retrieve the existing complaint from the repository
                var existingComplaint = _complaintRepository.GetById(complaintDTO.ComplaintId);

                if (existingComplaint != null)
                {
                    // Update the existing complaint with the new information
                    existingComplaint.Category = complaintDTO.Category;
                    existingComplaint.Description = complaintDTO.Description;
                    existingComplaint.OrganizationId = complaintDTO.OrganizationId;
                    existingComplaint.OrganizationName = complaintDTO.OrganizationName;
                    existingComplaint.Username = complaintDTO.Username;
                    existingComplaint.FilePath = complaintDTO.FilePath;

                    // Save the changes to the repository
                    _complaintRepository.Update(existingComplaint);

                    // Return the updated complaintDTO
                    return new ComplaintDTO
                    {
                        ComplaintId = existingComplaint.ComplaintId,
                        Category = existingComplaint.Category,
                        Description = existingComplaint.Description,
                        OrganizationId = existingComplaint.OrganizationId,
                        OrganizationName = existingComplaint.OrganizationName,
                        Username = existingComplaint.Username,
                        FilePath = existingComplaint.FilePath
                    };
                }
                else
                {
                    Console.WriteLine("Complaint not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating complaint: {ex.Message}");
                return null;
            }
        }
    }
}
