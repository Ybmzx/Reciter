using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reciter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Reciter.ViewModels
{
    public partial class SpellPageVM: ObservableObject
    {
        [ObservableProperty]
        public WordBook? wordBookData = null;
        [ObservableProperty]
        public Word? currentWord = null;
        [ObservableProperty]
        public int currentIndex = -1;
        [ObservableProperty]
        public string tipText = string.Empty;
        [ObservableProperty]
        public string inputText = string.Empty;

        HashSet<Word> mistakeWord = new();

        IContentDialogService contentDialogService;

        public SpellPageVM(IContentDialogService contentDialogService)
        {
            this.contentDialogService = contentDialogService;
        }

        [RelayCommand]
        async Task CheckAndChangeWord()
        {
            if (WordBookData is null) return;
            if (CurrentIndex >= WordBookData!.Words.Count - 1)
            {
                if (CurrentIndex == -1 || InputText.Trim() == CurrentWord!.Content)
                {
                    var tipString = string.Join(", \n", mistakeWord.Select(x => $"{x.Content}: {x.Translation}"));
                    await contentDialogService.ShowSimpleDialogAsync(
                        new SimpleContentDialogCreateOptions
                        {
                            Title = "结算!",
                            Content = mistakeWord.Count > 0 ? $"共有 {mistakeWord.Count} 个拼错了的单词, 它们分别是:\n{tipString}\n再接再厉!" : "一个错误都没有! 恭喜!",
                            CloseButtonText = "确定"
                        }
                    );
                    WordBookData = null;
                    InputText = string.Empty;
                    TipText = string.Empty;
                    CurrentWord = null;
                    CurrentIndex = -1;
                    mistakeWord.Clear();
                    return;
                }
                mistakeWord.Add(WordBookData.Words[CurrentIndex]);
                TipText = CurrentWord!.Content;
                return;
            }
            if (CurrentIndex == -1 || InputText.Trim() == CurrentWord!.Content)
            {
                CurrentIndex++;
                CurrentWord = WordBookData.Words[CurrentIndex];
                TipText = string.Empty;
                InputText = string.Empty;
                return;
            }
            mistakeWord.Add(WordBookData.Words[CurrentIndex]);
            TipText = CurrentWord!.Content;
        }

        [RelayCommand]
        async Task InputTextBoxKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                await CheckAndChangeWord();
            }
        }

        public async Task NavigatedTo()
        {
            if (WordBookData is null) return;
            mistakeWord.Clear();
            await CheckAndChangeWord();
        }

    }
}
