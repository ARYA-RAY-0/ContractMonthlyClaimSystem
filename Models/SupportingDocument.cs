namespace ContractMonthlyClaimSystem.Models
{
    public class SupportingDocument
    {
        public int SupportingDocumentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; } // To store the path to the uploaded file

        // Foreign Key to the parent Claim
        public int ClaimId { get; set; }
        public Claim Claim { get; set; } // Navigation property
    }
}
