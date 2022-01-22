#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FileService.Acu.DAC;
using PX.Data;

namespace FileService.Acu.Helper
{
    public class PathBuilder
    {
        public string GetPath(string entityType, PXGraph graph)
        {
            FileEntityMapping template =
                PXSelect<FileEntityMapping, Where<FileEntityMapping.entity, Equal<Required<FileEntityMapping.entity>>>>
                    .Select(graph, entityType);
            if (string.IsNullOrWhiteSpace(template?.Mapping)) 
                throw new ArgumentException("Entity mapping is not defined");

            var values = GetValuesFromGraph(GetValueIdsToExtract(template.Mapping), graph);

            string createdMapping = template.Mapping;
            foreach (FieldValue fieldValue in values)
            {
                createdMapping = createdMapping.Replace(fieldValue.ReplacementString, fieldValue.Value);
            }

            return createdMapping;
        }

        private IEnumerable<FieldValue> GetValueIdsToExtract(string mapping)
        {
            var matches = Regex.Matches(mapping, "{(?<table>[a-zA-Z]*).(?<field>[a-zA-Z]*)}");
            return matches.Cast<Match>().Select(m => new FieldValue()
            {
                ReplacementString = m.Value,
                Table = m.Groups["table"].Value,
                Field = m.Groups["field"].Value
            });
        }

        private IEnumerable<FieldValue> GetValuesFromGraph(IEnumerable<FieldValue> values, PXGraph graph)
        {
            foreach (FieldValue fieldValue in values)
            {
                PXTrace.WriteInformation($"Looking for current row for: {fieldValue.Table}");
                var row = graph.Caches[fieldValue.Table].Current;

                if (row is not null)
                {
                    PXTrace.WriteInformation($"Row Found for: {fieldValue.Table}");
                    fieldValue.Value = graph.Caches[fieldValue.Table].GetValue(row, fieldValue.Field).ToString();
                    PXTrace.WriteInformation($"Value Is: {fieldValue.Value}");
                }

                yield return fieldValue;
            }
        }

        private class FieldValue
        {
            public string ReplacementString { get; set; }
            public string Table { get; set; }
            public string Field { get; set; }
            public string Value { get; set; }
        }
    }
}