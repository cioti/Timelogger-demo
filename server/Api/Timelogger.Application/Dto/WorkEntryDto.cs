using System;

namespace Timelogger.Application.Dto
{
    public class WorkEntryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Hours { get; set; }
        public DateTime Date { get; set; }
    }
}
