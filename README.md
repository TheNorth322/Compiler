# О программе
Данная программа предназначена для синтаксического анализа кода введенного в редакторе или загруженного из файла. В верхней части приложения присутствует меню, а также кнопки для быстрого вызова необходимых функций. По центру экрана располагается редактор с нумерацией строк, возможностью увеличивать и уменьшать размер шрифта, а также подсветкой синтаксиса для языка C#. Присутствует возможность открыть несколько вкладок с файлами. Для каждой из вкладок меню ошибок своё.
## Функции приложения
- Файл (File)
  1. Создать (Create) - функция для создания окна редактирования файла. Для того чтобы сохранить его необходимо воспользоваться функцией "Сохранить как"
  2. Открыть (Open) - функция открывает проводник, в котором пользователь указывает файл, который необходимо открыть
  3. Сохранить (Save) - функция для сохранения изменений внесенный в файл
  4. Сохранить как (Save as) - функция для сохранения файла
  5. Выход (Exit) - функция для выхода из приложения. Если есть несохраненные изменения, будет предложено их сохранить.
- Правка (Edit)
  1. Отменить (Cancel) - функция для отмены внесенных изменений
  2. Повторить (Repeat) - функция для возвращения внесенных изменений
  3. Вырезать (Cut) - функция для вырезания выделенного текста
  4. Копировать (Copy) - функция для копирование выделенного текста
  5. Вставить (Paste) - функция вставляет текст из буфера обмена в редактор
  6. Удалить (Delete) - функция удаляет выделенный текст
  7. Выделить все (Select all) - функция выделяет весь текст в редакторе
- Справка (Reference)
  1. Вызов справки (Call reference) - функция открывает html-документ справки в браузере
  2. О программе (Program description) - функция открывает html-документ описания программы в браузере

# Контрольный список для оценки приложения
- [x] Приложение имеет графический интерфейс пользователя.
- [ ] Приложение запускается и корректно работает на компьютере без установленной среды разработки (IDE), все необходимые модули поставляются вместе с программой.
- [x] В заголовке окна отображается корректное название приложения.
- [x] Дизайн интерфейса соответствует примеру (см. раздел “Интерфейс текстового редактора”), содержит все необходимые элементы. 
- [ ] Программа корректно реализует все заданные функции меню “Файл”, “Правка” и “Справка” и панели инструментов.
- [x] Окно приложения корректно реагирует на изменение размера. Предусмотрены ограничения размера, если это необходимо.
- [x] Пользователь может изменять соотношение размеров области редактирования и области вывода результатов.
- [x] В окне редактирования и области вывода результатов, при необходимости, появляется полоса прокрутки.
- [x] При открытии нового файла или выходе из программы приложение должно предлагать сохранить изменения в файле, если пользователь редактировал текст.

# Дополнительное задание
- [x] Изменение размеров текста в окне редактирования и окне вывода результатов.
- [x] Интерфейс с вкладками, позволяющий одновременно работать с несколькими текстами (для окна редактирования).
- [x] Выбор языка интерфейса приложения (интернационализация).
- [x] Нумерация строк в окне редактирования текста.
- [x] Открытие файла при перетаскивании иконки в окно программы.
- [ ] Наличие строки состояния для отображения текущей информации о состоянии работы приложения.
- [x] Базовая подсветка синтаксиса в окне редактирования.
- [x] Интерфейс с вкладками, позволяющий работать с разными модулями программы (для окна вывода результатов)
- [x] Отображение ошибок в окне вывода результатов в виде таблицы.

# Демонстрация работы программы
![image](https://github.com/TheNorth322/Compiler/assets/70006380/ac0bad86-8f72-402c-997e-b805f4269763)
![image](https://github.com/TheNorth322/Compiler/assets/70006380/4d5dc308-1939-44ff-bafb-0e7ed85ad7ec)
![image](https://github.com/TheNorth322/Compiler/assets/70006380/425057f8-9b6d-4126-a318-cee4f32de9de)



