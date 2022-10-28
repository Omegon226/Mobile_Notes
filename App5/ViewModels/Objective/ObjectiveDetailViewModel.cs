using App5.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5.ViewModels.Objective
{
    [QueryProperty(nameof(ObjectiveId), nameof(ObjectiveId))]
    public class ObjectiveDetailViewModel : BaseViewModel
    {
        private string objectiveId;
        private string name;
        private string tag;
        private string prioirty;
        private DateTime dateToFinish;
        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Tag
        {
            get => tag;
            set => SetProperty(ref tag, value);
        }

        public string Priority
        {
            get => prioirty;
            set => SetProperty(ref prioirty, value);
        }

        public DateTime DateToFinish
        {
            get => dateToFinish;
            set => SetProperty(ref dateToFinish, value);
        }

        public string ObjectiveId
        {
            get
            {
                return objectiveId;
            }
            set
            {
                objectiveId = value;
                LoadObjectiveId(value);
            }
        }

        public async void LoadObjectiveId(string objectiveId)
        {
            try
            {
                var item = await ObjectiveDataStore.GetAsync(objectiveId);
                Id = item.Id;
                Name = item.Name;
                Tag = item.Tag;
                Priority = item.Priority;
                DateToFinish = item.DateToFinish;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Objective");
            }
        }
    }
}
