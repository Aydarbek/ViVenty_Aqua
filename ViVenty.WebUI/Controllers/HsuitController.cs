﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViVenty.Domain.Abstract;
using ViVenty.Domain.Entities;
using ViVenty.WebUI.Models;

namespace ViVenty.WebUI.Controllers
{
    public class HsuitController : Controller
    {
        // GET: Hsuit

        private IViventyRepository repository;
        public int pageSize { get; set; } = 3;

        public HsuitController(IViventyRepository repoParam)
        {
            repository = repoParam;
        }

        public ViewResult List(string category, int page = 1)
        {
            HsuitListViewModel model = new HsuitListViewModel
            {
                Hsuits = repository.
                Hsuits.Where(p => category == null || p.Category == category).
                OrderBy(hs => hs.Id).
                Skip((page - 1) * pageSize).
                Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Hsuits.Count() :
                    repository.Hsuits.Where(m => m.Category == category).Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }
    }
}