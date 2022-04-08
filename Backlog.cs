namespace BacklogsWeb
{
    public class Backlog
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public string? TargetDate { get; set; }
        public bool? IsComplete { get; set; }
        public string? ImageURL { get; set; }
        public string? TrailerURL { get; set; }
        public int? Progress { get; set; }
        public string? SearchURL { get; set; }
        public string? Units { get; set; }
        public string? Director { get; set; }
        public bool? ShowProgress { get; set; }
        public TimeSpan? NotifTime { get; set; }
        public bool? RemindEveryday  { get; set; }
        public double? UserRating { get; set; }
        public string? CreatedDate { get; set; }
        public string? CompletedDate { get; set; }
    }
}
