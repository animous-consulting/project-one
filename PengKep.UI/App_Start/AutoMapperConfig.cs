﻿using System;
using System.Collections.Generic;
using AutoMapper;

using PengKep.BusinessEntities;
using PengKep.ViewModels;

namespace PengKep
{
    public class AutoMapperConfig
    {
        public static void Initialise()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Approval, ApprovalViewModel>();
                cfg.CreateMap<ApprovalAction, ApprovalActionViewModel>();
                cfg.CreateMap<ApprovalStatus, ApprovalStatusViewModel>();
                cfg.CreateMap<ApprovalHistory, ApprovalHistoryViewModel>();
                cfg.CreateMap<Company, CompanyViewModel>();
                cfg.CreateMap<ExpenseCategory, ExpenseCategoryViewModel>();
                cfg.CreateMap<Expense, ExpenseViewModel>();
                cfg.CreateMap<OrganizationUnit, OrganizationUnitViewModel>();
                cfg.CreateMap<PMExpense, PMExpenseViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
               
            });
        }
    }
}