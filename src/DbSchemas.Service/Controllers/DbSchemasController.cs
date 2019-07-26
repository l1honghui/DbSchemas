using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbSchemas.Service.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class DbSchemasController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly DbSchemasService _dbSchemasService  = new DbSchemasService();

        /// <summary>
        /// 获取所有schemas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetSchemas()
        {
            try
            {
                var result = await _dbSchemasService.GetSchemas();
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }


        /// <summary>
        /// 获取schema所有table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetSchemaTables(string schemaTable)
        {
            try
            {
                string schemas = null;
                string tablename = null;
                if (!string.IsNullOrEmpty(schemaTable))
                {
                    if (schemaTable.Contains("."))
                    {
                        var tableArr = schemaTable.Split('.');
                        schemas = tableArr[0];
                        tablename = tableArr[1];
                    }
                    else
                        tablename = schemaTable;
                }

                var result = await _dbSchemasService.GetTable(schemas, tablename);
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }

        /// <summary>
        /// 获取table所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseData<List<dynamic>>>> GetTableInfos(string tableName)
        {
            try
            {
                var tablestrArr = tableName.Split('.');
                var querytable = tablestrArr.Length == 2 ? tablestrArr[1] : tableName;
                var result = await _dbSchemasService.GetTableInfos(querytable);
                return ResponseData<List<dynamic>>.Success(result);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                Console.WriteLine(e);
                return ResponseData<List<dynamic>>.Failure(500, e.Message, null);
            }
        }
    }

}