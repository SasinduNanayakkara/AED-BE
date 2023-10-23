using System;
using AED_BE.Models;
namespace AED_BE.DTO.ResponseDto
{
    public class FilterTrain{

        public String id { get; set; }
        public int trainNo { get; set; }
        public String name { get; set; }
        public List<String> date { get; set; }
        public List<Stations> stations { get; set; }
        public Stations startStation { get; set; }
        public Stations endStation { get; set; }
}

    
}

