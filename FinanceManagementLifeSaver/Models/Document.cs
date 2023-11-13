namespace FinanceManagementLifesaver.Models
{
    public class Document
    {
        public int Id { get; set; }
        public Transaction TransactionId { get; set; }  
        public string Path { get; set; }    
    }
}
