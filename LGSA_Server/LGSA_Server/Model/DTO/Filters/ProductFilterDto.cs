﻿using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LGSA_Server.Model.DTO.Filters
{
    public class ProductFilterDto : IFilterDto<product>
    {
        public string ProductName { get; set; }
        public int? GenreId { get; set; }
        public int? ConditionId { get; set; }
        public int? ProductTypeId { get; set; }
        [Range(0, 5)]
        public double? Rating { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Stock { get; set; }
        [Range(0, int.MaxValue)]
        public int? SoldCopies { get; set; }

        public Expression<Func<product, bool>> GetFilter(int userId)
        {
            var builder = PredicateBuilder.New<product>();
            builder.And(p => p.product_owner == userId && p.stock >= Stock);
            if(Rating != null)
            {
                builder.And(p => p.rating >= Rating || p.rating == null);
            }
            if(ProductName != null)
            {
                builder.And(p => p.Name.Contains(ProductName));
            }
            if(SoldCopies != null)
            {
                builder.And(p => p.sold_copies >= SoldCopies);
            }
            if(ConditionId != null)
            {
                builder.And(p => p.condition_id == ConditionId);
            }
            if(GenreId != null)
            {
                builder.And(p => p.genre_id == GenreId);
            }
            if(ProductTypeId != null)
            {
                builder.And(p => p.product_type_id == ProductTypeId);
            }

            return builder;
        }
    }
}