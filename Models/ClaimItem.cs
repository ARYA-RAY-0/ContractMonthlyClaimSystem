namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimItem
    {
        public int ClaimItemId { get; set; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public string Description { get; set; }

        // Foreign Key to the parent Claim
        public int ClaimId { get; set; }
        public Claim Claim { get; set; } // Navigation property
    }
}
