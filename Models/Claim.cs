using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Required for [NotMapped]
using Microsoft.AspNetCore.Http; // Required for IFormFile
using Microsoft.AspNetCore.Identity;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public DateTime ClaimMonth { get; set; } // e.g., 2025-09-01
        public double TotalHours { get; set; }
        public decimal TotalAmount { get; set; }
        public ClaimStatus Status { get; set; } // Uses the enum below

        // Foreign Key to the User who created this claim
        public string UserId { get; set; }
        public IdentityUser User { get; set; } // Use the built-in class

        // A Claim has many Items and Documents
        public virtual ICollection<ClaimItem> ClaimItems { get; set; }
        public virtual ICollection<SupportingDocument> SupportingDocuments { get; set; }

        // These properties are NOT for the database. They are only for the UI form.
        [NotMapped] // Tells Entity Framework to ignore this property
        public string NewItemDescription { get; set; }
        [NotMapped]
        public double NewItemHours { get; set; }
        [NotMapped]
        public IFormFile UploadedFile { get; set; } // For the file upload input
    }

    public enum ClaimStatus
    {
        Submitted, // Lecturer submits it
        ApprovedByCoordinator,
        ApprovedByManager,
        Rejected
    }
}