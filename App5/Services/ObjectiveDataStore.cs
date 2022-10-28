using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App5.Models;

namespace App5.Services
{
    public class ObjectiveDataStore : IDataStore<Objective>
    {
        readonly List<Models.Objective> objectives = new List<Models.Objective>();
        const string _objectivesJsonFileName = "Objective.json";

        #region Строка JSON для Objectives
        string jsonObjectivesText = @"[
            {
                'Id': 1,
                'Name': Задача 1
                'Tag': 'В работе',
                'DateToFinish': '10/27/2022',
                'LinksToOtherTasks': {}
            },
            {
                'Id': 2,
                'Name': Задача 2
                'Tag': 'В ожидании',
                'DateToFinish': '10/27/2022',
                'LinksToOtherTasks': {}
            },
            {
                'Id': 3,
                'Name': Задача 3
                'Tag': 'Без метки',
                'DateToFinish': '10/28/2022',
                'LinksToOtherTasks': {}
            }
            ]";
        #endregion

        string pathPersonal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public ObjectiveDataStore()
        {
            //var path_objectives = Path.Combine(pathPersonal, _objectivesJsonFileName);
            //File.WriteAllText(path_objectives, jsonObjectivesText);
            //var jsonObjectivesContent = File.ReadAllText(path_objectives);

            //objectives.AddRange(JsonConvert.DeserializeObject<List<Models.Objective>>(jsonObjectivesContent));

            objectives = new List<Objective>()
            {
                new Objective { Id = Guid.NewGuid().ToString(), Name = "Задача 1", Tag = "В работе", Priority = "Высокая", DateToFinish = new DateTime(2022, 10, 27), LinksToOtherObjectives = new List<Objective>() },
                new Objective { Id = Guid.NewGuid().ToString(), Name = "Задача 2", Tag = "В ожидании", Priority = "Средняя", DateToFinish = new DateTime(2022, 10, 27), LinksToOtherObjectives = new List<Objective>() },
                new Objective { Id = Guid.NewGuid().ToString(), Name = "Задача 3", Tag = "Без метки", Priority = "Низкая", DateToFinish = new DateTime(2022, 10, 28), LinksToOtherObjectives = new List<Objective>() },
            };
        }

        public async Task<bool> AddAsync(Objective objective)
        {
            objectives.Add(objective);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Objective objective)
        {
            var oldItem = objectives.Where((Objective arg) => arg.Id == objective.Id).FirstOrDefault();
            objectives.Remove(oldItem);
            objectives.Add(objective);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = objectives.Where((Objective arg) => arg.Id == id).FirstOrDefault();
            objectives.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Objective> GetAsync(string id)
        {
            return await Task.FromResult(objectives.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Objective>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(objectives);
        }
    }
}
