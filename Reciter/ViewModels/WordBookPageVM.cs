using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reciter.Controls;
using Reciter.Models;
using Reciter.Services;
using Reciter.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Reciter.ViewModels
{
    public partial class WordBookPageVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<WordBook> wordBooks;
        [ObservableProperty]
        WordBook? selectedWordBook;
        [ObservableProperty]
        int selectedWordBookIndex;
        [ObservableProperty]
        bool selectedWordBookChanged = false;

        IDataServices dataServices;
        IContentDialogService contentDialogService;
        INavigationService navigationService;

        public WordBookPageVM(IDataServices dataServices, IContentDialogService contentDialog, INavigationService navigationService) {
            this.dataServices = dataServices;
            this.contentDialogService = contentDialog;
            this.navigationService = navigationService;
            wordBooks = new(dataServices.GetWordBooks());
            SelectedWordBookIndex = -1;
        }

        [RelayCommand]
        async Task DeleteWordBook()
        {
            if (SelectedWordBookIndex < 0)
            {
                await contentDialogService.ShowSimpleDialogAsync(
                    new SimpleContentDialogCreateOptions
                    {
                        Title = "提示",
                        Content = "请选择要删除的单词本",
                        CloseButtonText = "确定"
                    });
                return;
            }
            ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions
                {
                    Title = "你确定要删除吗?",
                    Content = "你确定要删除吗, 该操作无法撤回.",
                    PrimaryButtonText = "确定",
                    CloseButtonText = "取消"
                }
            );
            if (result != ContentDialogResult.Primary) return;
            dataServices.RemoveAt(SelectedWordBookIndex);
            WordBooks.RemoveAt(SelectedWordBookIndex);
            dataServices.Save();
        }

        [RelayCommand]
        void AddWordBook()
        {
            WordBooks.Add(dataServices.Add(new WordBook { Name = "未命名单词本" }));
            dataServices.Save();
        }

        [RelayCommand]
        async Task StartSpell()
        {
            var practiceConfig = new PracticeConfigDialog(contentDialogService.GetDialogHost()!);
            var result = await practiceConfig.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                SpellPageVM vm = new(contentDialogService);
                vm.WordBookData = new WordBook();
                vm.WordBookData.Name = WordBooks[SelectedWordBookIndex].Name;
                vm.WordBookData.Words = new(WordBooks[SelectedWordBookIndex].Words
                    .Where(x => x.IsSelected || practiceConfig.Config.Selection == PracticeConfig.SelectionType.All));
                if (practiceConfig.Config.Order == PracticeConfig.OrderType.Descending)
                    vm.WordBookData.Words = new(vm.WordBookData.Words.Reverse());
                Random random = new Random();
                if (practiceConfig.Config.Order == PracticeConfig.OrderType.Random)
                    vm.WordBookData.Words = new(vm.WordBookData.Words.OrderBy(x => random.Next()));
                navigationService.Navigate(typeof(SpellPage), vm);
            }
        }

        [RelayCommand]
        void UpdateWordBook()
        {
            dataServices.Save();
        }

        [RelayCommand]
        void SelectAllWords()
        {
            if (SelectedWordBookIndex < 0) return;
            foreach (var word in WordBooks[SelectedWordBookIndex].Words)
            {
                word.IsSelected = true;
            }
        }

        [RelayCommand]
        void UnSelectAllWords()
        {
            if (SelectedWordBookIndex < 0) return;
            foreach (var word in WordBooks[SelectedWordBookIndex].Words)
            {
                word.IsSelected = false;
            }
        }

        [RelayCommand]
        void AddWord()
        {
            if (SelectedWordBookIndex < 0) return;
            WordBooks[SelectedWordBookIndex].Words.Add(new Word());
            dataServices.Save();
        }

        [RelayCommand]
        void WorkBookChanged()
        {
            SelectedWordBookChanged = true;
            dataServices.Save();
        }

        [RelayCommand]
        void DeleteWord(int index)
        {
            if (SelectedWordBookIndex == -1 || SelectedWordBook is null) return;
            dataServices.At(SelectedWordBookIndex).Words.RemoveAt(index);
            WorkBookChanged();
        }

        [RelayCommand]
        void WordBookSelectionChanged(SelectionChangedEventArgs e)
        {
            WordBookSelectionChanged(e.AddedItems.Count > 0 ? (WordBook?)e.AddedItems[0] : null);
        }

        void WordBookSelectionChanged(WordBook? wordBook)
        {
            this.SelectedWordBook = wordBook;
            this.SelectedWordBookChanged = false;
        }

    }
}
