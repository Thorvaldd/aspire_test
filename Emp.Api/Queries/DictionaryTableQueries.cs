namespace Emp.Api.Queries;

public static class DictionaryTableQueries
{
    public static readonly string GetPositions = 
        @"SELECT 
            p.position_id AS 'Key',
            p.title AS 'Value'
    FROM
        dict_position p;";
}