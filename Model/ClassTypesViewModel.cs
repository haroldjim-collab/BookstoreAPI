﻿namespace BookstoreAPI
{
    public class ClassTypesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }

        public CategoryTypesViewModel CategoryTypes { get; set; }        
    }
}
