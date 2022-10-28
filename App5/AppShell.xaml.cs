using App5.ViewModels;
using App5.Views;
using App5.Views.Note;
using App5.Views.Objective;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App5
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ObjectiveDetailPage), typeof(ObjectiveDetailPage));
            Routing.RegisterRoute(nameof(NewObjectivePage), typeof(NewObjectivePage));
            Routing.RegisterRoute(nameof(FindeObjectivePage), typeof(FindeObjectivePage));
            Routing.RegisterRoute(nameof(BoardObjectivesPage), typeof(BoardObjectivesPage));
            Routing.RegisterRoute(nameof(AdditionalInfoObjectivePage), typeof(AdditionalInfoObjectivePage));
            Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));
            Routing.RegisterRoute(nameof(NewNotePage), typeof(NewNotePage));
            Routing.RegisterRoute(nameof(FindNotePage), typeof(FindNotePage));
        }

    }
}
