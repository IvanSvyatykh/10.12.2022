using System;
using System.Collections.Generic;

namespace UselessClasses
{
    class Menu
    {
        public List<MenuEntry> Entries = new List<MenuEntry>();

        public void HandleChoise(int entryIndex)
        {
            MenuEntry entry = Entries[entryIndex];
            entry.ProceedMenuAction();
        }
    }
    abstract class MenuEntry
    {
        public string EntryName { get; }

        public MenuEntry(string entryName)
        {
            this.EntryName = entryName;
        }

        public abstract void ProceedMenuAction();
    }
    class PlayGame : MenuEntry
    {
        public PlayGame(string entryName) : base(entryName) { }

        public override void ProceedMenuAction()
        {
            Console.WriteLine("Игра началась");
        }
    }
    class ExitGame : MenuEntry
    {
        public ExitGame(string entryName) : base(entryName) { }

        public override void ProceedMenuAction()
        {
            Exit();
        }

        private void Exit()
        {
            Console.WriteLine("Произошёл выход");
            Environment.Exit(0);
        }
    }
    class Options : MenuEntry
    {
        public Options(string entryName) : base(entryName) { }
        public override void ProceedMenuAction()
        {
            Console.WriteLine("На экране вы видите опции");
        }
    }

    abstract class KeyReader
    {
        public ConsoleKeyInfo KeyInfo { get; }

        public KeyReader (ConsoleKeyInfo keyInfo)
        {
            this.KeyInfo= keyInfo;
        }
        public abstract void MenuLogic();
    }

    class UpMover : KeyReader
    {
        public UpMover(ConsoleKeyInfo keyInfo) : base(keyInfo) { }

        public override void MenuLogic()
        {
            Console.WriteLine("Игра началась");
        }

    }
    class DownMover : KeyReader
    {
        public DownMover(ConsoleKeyInfo keyInfo) : base(keyInfo) { }

        public override void MenuLogic()
        {
            Console.WriteLine("Игра началась");
        }
    }
    class PressButton : KeyReader
    {
        public PressButton(ConsoleKeyInfo keyInfo) : base(keyInfo) { }

        public override void MenuLogic()
        {
            Console.WriteLine("Игра началась");
        }

    }
    class MenuDrawer
    {
        private Menu _menu;
        private int _selectedPosition = 0;
        private const int PADDING = 2;
        private const int BULLET = 1;

        public MenuDrawer(Menu menu)
        {
            _menu = menu;
        }

        public void Draw()
        {
            int maxLength = CalculateMaxLenghth();
            string separator = new String('-', maxLength + PADDING + BULLET);
            Console.WriteLine(separator);
            for (int i = 0; i < _menu.Entries.Count; i++)
            {
                MenuEntry entry = _menu.Entries[i];
                char bullet = i == _selectedPosition
                    ? '*'
                    : ' ';
                Console.WriteLine($"|{bullet}{entry.EntryName.PadRight(maxLength)}|");
                Console.WriteLine(separator);
            }
        }

        /// <summary>
        /// Ваша задача - исправить этот метод таким образом, чтобы с ним можно было работать без использования
        /// ветвления посредством if-elseif и других условных операторов
        /// </summary>

        public void InteractWithUser()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                _selectedPosition--;
                if (_selectedPosition < 0)
                {
                    _selectedPosition = _menu.Entries.Count - 1;
                }
                Console.Clear();
                Draw();
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                _selectedPosition++;
                if (_selectedPosition >= _menu.Entries.Count)
                {
                    _selectedPosition = 0;
                }
                Console.Clear();
                Draw();
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                _menu.HandleChoise(_selectedPosition);
            }
        }
        private int CalculateMaxLenghth()
        {
            int maxLength = 0;
            foreach (MenuEntry entry in _menu.Entries)
            {
                if (entry.EntryName.Length > maxLength)
                {
                    maxLength = entry.EntryName.Length;
                }
            }
            return maxLength;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Entries.Add(new PlayGame("Играть"));
            menu.Entries.Add(new Options("Опции"));
            menu.Entries.Add(new ExitGame("Выйти"));
            MenuDrawer drawer = new MenuDrawer(menu);
            drawer.Draw();
            while (true)
            {
                drawer.InteractWithUser();
            }
        }
    }
}