//using DataModels.EntityModels.ERPModel;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

using Microsoft.AspNetCore.Http;
//using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Reporting.NETCore;
//using AspNetCore.Reporting;
//using DataModels.ViewModels;

namespace Technofair.Lib.Utilities
{
    public class ReportingService
    {
        public static object Report(List<DataTable> dataList, string paramList, string filePath, string repType)
        {
            object bytes = null; string mimeType = string.Empty, paramSetName = "ParamSet", dataSetName = string.Empty; int extension = 1;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    //Initialize local report with path
                    LocalReport localReport = new LocalReport();
                    localReport.ReportPath = filePath;

                    //Set Parameters
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    DataTable parameterList = Extension.GetDataTable(paramList);

                    //Add DataSource
                    foreach (var dt in dataList)
                    {
                        dataSetName = dt.Rows[0].Field<string>(Extension.dataColumn);
                        localReport.DataSources.Add(new ReportDataSource(dataSetName, dt));
                    }

                    localReport.DataSources.Add(new ReportDataSource(paramSetName, parameterList));

                    //Get values by type
                    bytes = localReport.Render(repType);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return bytes;
        }

        public static object Report(List<DataTable> dataList, string filePath, string repType)
        {
            object bytes = null; string mimeType = string.Empty, paramSetName = "ParamSet", dataSetName = string.Empty; int extension = 1;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    //Initialize local report with path
                    LocalReport localReport = new LocalReport();
                    localReport.ReportPath = filePath;

                    //Set Parameters
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    DataTable parameterList = new DataTable();

                    //Add DataSource
                    foreach (var dt in dataList)
                    {
                        dataSetName = dt.Rows[0].Field<string>(Extension.dataColumn);
                        localReport.DataSources.Add(new ReportDataSource(dataSetName, dt));
                    }

                    localReport.DataSources.Add(new ReportDataSource(paramSetName, parameterList));

                    //Get values by type
                    bytes = localReport.Render(repType);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return bytes;
        }

        public static object Report(List<DataTable> dataList, DataTable paramList, string filePath, string repType)
        {
            object bytes = null; string mimeType = string.Empty, dataSetName = string.Empty;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    //Initialize local report with path
                    LocalReport localReport = new LocalReport();
                    localReport.ReportPath = filePath;

                    //Set Parameters
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    //DataTable parameterList = Extension.GetDataTable(paramList);
                    localReport.DataSources.Clear();
                    //Add DataSource
                    foreach (var dt in dataList)
                    {
                        dataSetName = dt.Rows[0].Field<string>(Extension.dataColumn);
                        localReport.DataSources.Add(new ReportDataSource(dataSetName, dt));
                    }

                    localReport.EnableExternalImages = true;

                    if (paramList.Rows.Count > 0)
                    {
                        foreach (DataRow dr in paramList.Rows)
                        {
                            foreach (DataColumn cols in paramList.Columns)
                            {
                                string col = cols.ColumnName;
                                string val = dr[cols.ColumnName].ToString();
                                localReport.SetParameters(new ReportParameter(col, val));
                            }
                        }

                    }
                    //Get values by type
                    bytes = localReport.Render(repType);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return bytes;
        }
    }
}