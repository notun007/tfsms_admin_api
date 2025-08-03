using TFSMS.Admin.Model.Common;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Common
{

    public interface ICmnMonthService
    {
        List<CmnMonth> GetAll();
        List<CmnMonth> GetMonthByYearId(int yearId);
        List<DateTime> GetYearMonthDatesByYearId(int yearId);
        List<DateTime> GetCurrentYearMonthDates();
        List<CmnMonth> GetFinancialYearMonth();
        List<DateTime> GetCurrentFinancialYearMonthDates();
        List<CmnMonth> GetFinancialYearMonthByYearId(int yearId);
        List<DateTime> GetFinancialYearMonthDatesByYearId(int yearId);
    }
    public class CmnMonthService : ICmnMonthService
    {
        private ICmnMonthRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public CmnMonthService(ICmnMonthRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<CmnMonth> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public List<CmnMonth> GetMonthByYearId(int yearId)
        {
            return repository.GetMonthByYearId(yearId);
        }
        public List<DateTime> GetYearMonthDatesByYearId(int yearId)
        {
            return repository.GetYearMonthDatesByYearId(yearId);
        }
        public List<DateTime> GetCurrentYearMonthDates()
        {
            return repository.GetCurrentYearMonthDates();
        }
        public List<CmnMonth> GetFinancialYearMonth()
        {
            return repository.GetFinancialYearMonth();
        }
        public List<DateTime> GetCurrentFinancialYearMonthDates()
        {
            return repository.GetCurrentFinancialYearMonthDates();
        }

        public List<CmnMonth> GetFinancialYearMonthByYearId(int yearId)
        {
            return repository.GetFinancialYearMonthByYearId(yearId);
        }
        public List<DateTime> GetFinancialYearMonthDatesByYearId(int yearId)
        {
            return repository.GetFinancialYearMonthDatesByYearId(yearId);
        }


    }
}

