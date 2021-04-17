// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    delegate dynamic FOTA_delegate();

    /*
     * Класс MainForm — основной класс программы.
     */
    /// <summary>
    /// Класс <c>MainForm</c> — основной класс программы.
    /// </summary>
    /// <remarks>
    /// Этот класс задает основную функциональность программы.
    /// </remarks>
    public partial class MainForm : Form
    {
        // Расстояние между элементами NumericUpDown и TextBox, которые содержат значения TM-1 Reading и силы натяжения спиц.
        private readonly int stepBetweenControls = 3;
    
        // Строка подключения к БД.
        private readonly string connectionString = "Data Source=wheel_tension.sqlite3;Version=3;";

        // Флаг зажатия левой кнопки мыши на TrackBar допустимого натяжения спиц.
        private bool isTrackbarMouseDown = false;
        // Флаг скроллинга TrackBar допустимого натяжения спиц.
        private bool isTrackbarScrolling = false;

        // Свойства NumericUpDown - TM-1 Reading.
        private readonly List<string> numericUpDownProperties = new List<string>() { "Minimum", "Maximum", "DecimalPlaces", "Increment", "Size" };
        // Свойства TextBox - силы натяжения спиц.
        private readonly List<string> textBoxProperties = new List<string>() { "Enabled", "Size" };
        // Свойства Label - порядкового номера спицы.
        private readonly List<string> labelProperties = new List<string>() { "Size" };

        // Словарь параметров для выполнения параметризированных запросов к БД.
        private Dictionary<string, string> parameters = new Dictionary<string, string>() { };

        // Объект Database.
        private Database database = new Database();
        // Объкт FormControls.
        private FormControls formControl = new FormControls();
        // Объект TensionChart.
        private TensionChart tensionChart = new TensionChart();
        // Объект ParameterCalculations.
        private ParameterCalculations parameterCalculations = new ParameterCalculations();

        public MainForm()
        {
            InitializeComponent();
            Text = $"{Application.ProductName} {Application.ProductVersion}"; // Название приложения.
            Update();
        }

        // Отключение WS_CLIPCHILDREN для исправления мерцания элементов управления формы.
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;
                return parms;
            }
        }

        // Событие загрузки формы.
        /// <summary>
        /// Событие загрузки формы.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // SQL запрос для получения списка материалов спиц.
            var materialsListCommand = @"
                SELECT material
                FROM tm1_conversion_table
                GROUP BY material";

            // Выполнение SQL запроса к БД и формирование массива материалов спиц.
            List<string> materialsList = database.ExecuteSelectQuery(connectionString, materialsListCommand, parameters);

            // Заполнение выпадающего списка материалов спиц.
            formControl.SetComboBoxValue(materialComboBox, materialsList, true, true);

            Task.Run(async () => {
                var author = "snikitin-de";
                var repositoryName = "Wheel-Tension-Application";
                var appName = "Wheel Tension Application";

                var FOTA = new FOTA(author, repositoryName, repositoryName, appName);

                await FOTA.getReleases();

                string latestAssetsName = FOTA.getLatestAssetsName();

                string latestTagName = FOTA.getLatestTagName();

                bool isUpdateNeeded = FOTA.isUpdateNeeded();

                if (isUpdateNeeded && !string.IsNullOrEmpty(latestTagName))
                {
                    Updater.FOTA = FOTA;

                    // Задаем иконку всплывающей подсказки
                    updaterNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    // Задаем текст подсказки
                    updaterNotifyIcon.BalloonTipText = $"Version {latestTagName.Replace("v", "")} is available. Click to update.";
                    // Устанавливаем зголовк
                    updaterNotifyIcon.BalloonTipTitle = $"Update";
                    // Отображаем подсказку 5 секунд
                    updaterNotifyIcon.ShowBalloonTip(5);
                }
            });
        }

        // Событие изменения выбранного материала спиц.
        /// <summary>
        /// Событие изменения выбранного материала спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void materialComboBox_TextChanged(object sender, EventArgs e)
        {
            // Получение значения выбранного материала спиц.
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);

            // SQL запрос для получения списка форм спиц в соответствии с материалом.
            var shapesListCommand = @"
                SELECT shape
                FROM tm1_conversion_table
                WHERE material = @material
                GROUP BY shape";

            // Параметры SQL запроса.
            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected }
            };

            // Выполнение SQL запроса к БД и формирование массива форм спиц.
            List<string> shapesList = database.ExecuteSelectQuery(connectionString, shapesListCommand, parameters);

            // Заполнение выпадающего списка форм спиц.
            formControl.SetComboBoxValue(shapeComboBox, shapesList, true, true);
            // Разблокировка выпадающего списка толщины спиц.
            formControl.SetComboBoxValue(thicknessComboBox, null, false, false);
        }

        // Событие изменения выбранной формы спиц.
        /// <summary>
        /// Событие изменения выбранной формы спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void shapeComboBox_TextChanged(object sender, EventArgs e)
        {
            // Получение значения выбранного материала спиц.
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            // Получение значения выбранной формы спиц.
            var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);

            // SQL запрос для получения списка толщины спиц в соответствии с материалом и формой.
            var thicknessListCommand = @"
                SELECT thickness
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape
                GROUP BY thickness";

            // Параметры SQL запроса.
            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected },
                { "@shape", shapeComboBoxSelected }
            };

            // Выполнение SQL запроса к БД и формирование массива толщины спиц.
            List<string> thicknessList = database.ExecuteSelectQuery(connectionString, thicknessListCommand, parameters);

            // Заполнение выпадающего списка толщины спиц.
            formControl.SetComboBoxValue(thicknessComboBox, thicknessList, true, true);
        }

        // Событие изменения выбранной толщины спиц.
        /// <summary>
        /// Событие изменения выбранной толщины спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void thicknessComboBox_TextChanged(object sender, EventArgs e)
        {
            // Получение значения выбранного материала спиц.
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            // Получение значения выбранной формы спиц.
            var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);
            // Получение значения выбранной толщины спиц.
            var thicknessComboBoxSelected = thicknessComboBox.GetItemText(thicknessComboBox.SelectedItem);

            // SQL запрос для получения списка TM-1 Reading в соответствии с параметрами спиц.
            var tm1ListCommand = @"
                SELECT tm1_deflection_reading
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tm1_deflection_reading";
            // SQL запрос для получения списка силы натяжения спиц в соответствии с их параметрами.
            var tensionListCommand = @"
                SELECT tension
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tension";

            // Параметры SQL запроса.
            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected },
                { "@shape", shapeComboBoxSelected },
                { "@thickness", thicknessComboBoxSelected }
            };

            // Выполнение SQL запроса к БД и формирование массива TM-1 Reading.
            List<string> tm1Deflection = database.ExecuteSelectQuery(connectionString, tm1ListCommand, parameters);
            // Выполнение SQL запроса к БД и формирование массива силы натяжения спиц.
            List<string> tension = database.ExecuteSelectQuery(connectionString, tensionListCommand, parameters);

            // Заголовки таблицы TM-1 Reading и допустимой силы натяжения спиц.
            var rowHeaders = new string[] { "TM-1 READING", "SPOKE TENSION (KGF)" };
            // Строки таблицы.
            var rows = new List<string[]> { tm1Deflection.ToArray(), tension.ToArray() };

            // Заполнение таблицы DataGridView.
            formControl.SetDataGridViewValues(conversionTableGridView, tm1Deflection.Count, rowHeaders, rows);

            // Включение элементов указания TM-1 Reading спиц.
            leftSideSpokeCountComboBox.Enabled = true;
            rightSideSpokeCountComboBox.Enabled = true;
            varianceTrackBar.Enabled = true;
        }

        // Событие изменения количества спиц левой стороны колеса.
        /// <summary>
        /// Событие изменения количества спиц левой стороны колеса.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void leftSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            // Получение значения количества спиц левой стороны колеса.
            var leftSideSpokesCount = Int16.Parse(leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem));

            // Расчет отступа от выпадающего списка с количеством спиц левой стороны колеса.
            int indentFromComboBox = formControl.GetOffsetFromControl(leftSideSpokeCountComboBox) + stepBetweenControls;

            // Добавление NumericUpDown в GroupBox для ввода TM-1 Reading.
            formControl.AddNumericUpDownToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesTm1ReadingNumericUpDown",
                numericUpDownProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                leftSideSpokeCountComboBox.Location.X,
                indentFromComboBox
            );

            // Добавление TextBox в GroupBox для отображения силы натяжения спиц.
            formControl.AddTextBoxToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesTensionTextBox",
                textBoxProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );

            // Добавление Label в GroupBox для отображения порядкового номера спицы.
            formControl.AddLabelToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesNumberLabel",
                labelProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                leftSideSpokesNumLabel.Location.X,
                indentFromComboBox + leftSideSpokeCountComboBox.Size.Height / 2 - leftSideSpokesNumLabel.Height / 2
            );

            foreach (NumericUpDown item in leftSideSpokesGroupBox.Controls.OfType<NumericUpDown>())
            {
                if (item.Name.IndexOf("leftSideSpokesTm1ReadingNumericUpDown") > -1)
                {
                    item.Enter += new EventHandler(makeNumericUpDownSelection);
                }
            }
        }

        // Выделение значения NumericUpDown по табу на NumericUpDown.
        /// <summary>
        /// Выделение значения NumericUpDown по табу на NumericUpDown.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void makeNumericUpDownSelection(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;

            int length = numericUpDown.Value.ToString().Length;
            numericUpDown.Select(0, length);
        }

        // Событие изменения количества спиц правой стороны колеса.
        /// <summary>
        /// Событие изменения количества спиц правой стороны колеса.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void rightSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            // Получение значения количества спиц правой стороны колеса.
            var rightSideSpokesCount = Int16.Parse(rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem));

            // Расчет отступа от выпадающего списка с количеством спиц правой стороны колеса.
            int indentFromComboBox = formControl.GetOffsetFromControl(rightSideSpokeCountComboBox) + stepBetweenControls;

            // Добавление NumericUpDown в GroupBox для ввода TM-1 Reading.
            formControl.AddNumericUpDownToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesTm1ReadingNumericUpDown",
                numericUpDownProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                rightSideSpokeCountComboBox.Location.X,
                indentFromComboBox
            );

            // Добавление TextBox в GroupBox для отображения силы натяжения спиц.
            formControl.AddTextBoxToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesTensionTextBox",
                textBoxProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );

            // Добавление Label в GroupBox для отображения порядкового номера спицы.
            formControl.AddLabelToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesNumberLabel",
                labelProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                rightSideSpokesNumLabel.Location.X,
                indentFromComboBox + rightSideSpokeCountComboBox.Size.Height / 2 - rightSideSpokesNumLabel.Height / 2
            );

            foreach (NumericUpDown item in rightSideSpokesGroupBox.Controls.OfType<NumericUpDown>())
            {
                if (item.Name.IndexOf("rightSideSpokesTm1ReadingNumericUpDown") > -1)
                {
                    item.Enter += new EventHandler(makeNumericUpDownSelection);
                }
            }
        }

        // Проведение расчетов и построение графиков.
        /// <summary>
        /// Проведение расчетов и построение графиков.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            // Получение значение количества спиц левой стороны колеса.
            var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);
            // Получение значение количества спиц ghfdjq стороны колеса.
            var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

            // Если количество спиц не выбрано, то выводим сообщение об ошибке.
            if (String.IsNullOrEmpty(leftSideSpokeCountComboBoxSelected) || String.IsNullOrEmpty(rightSideSpokeCountComboBoxSelected))
            {
                MessageBox.Show("Number of spokes not selected!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Если количество спиц на левой и правой стороне колеса не совпадают, то выводим сообщение об ошибке.
                if (leftSideSpokeCountComboBoxSelected != rightSideSpokeCountComboBoxSelected)
                {
                    MessageBox.Show("Your wheel isn't symmetrical!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Массив TM-1 Reading спиц левой стороны колеса.
                List<float> leftSideSpokesTm1 = formControl.GetValuesFromGroupControls(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown").Select(x => float.Parse(x.Value)).ToList();

                // Массив TM-1 Reading спиц правой стороны колеса.
                List<float> rightSideSpokesTm1 = formControl.GetValuesFromGroupControls(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown").Select(x => float.Parse(x.Value)).ToList();

                // Углы положения спиц левой стороны колеса.
                List<float> leftSpokesAngles = parameterCalculations.CalculateSpokeAngles(byte.Parse(leftSideSpokeCountComboBox.Text));
                // Углы положения спиц правой стороны колеса.
                List<float> rightSpokesAngles = parameterCalculations.CalculateSpokeAngles(byte.Parse(rightSideSpokeCountComboBox.Text));

                // Массив силы натяжения спиц левой стороны колеса.
                float[] leftSideSpokesTensionKgf = parameterCalculations.CalculateTensionKgf(
                    conversionTableGridView,
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown");

                // Массив силы натяжения спиц правой стороны колеса.
                float[] rightSideSpokesTensionKgf = parameterCalculations.CalculateTensionKgf(
                    conversionTableGridView,
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown");

                // Очистка диаграммы.
                spokeTensionChart.Series.Clear();

                // Расчет масштаба графика.
                spokeTensionChart.ChartAreas["ChartArea"].AxisY.Maximum = new List<float> { leftSideSpokesTm1.Max(), rightSideSpokesTm1.Max() }.Max() * 2.0;

                // Установка рассчитанного натяжения спиц левой стороны колеса.
                formControl.SetValuesToGroupControlsText(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTensionTextBox",
                    leftSideSpokesTensionKgf.Select(x => x.ToString()).ToArray());

                // Установка рассчитанного натяжения спиц правой стороны колеса.
                formControl.SetValuesToGroupControlsText(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTensionTextBox",
                    rightSideSpokesTensionKgf.Select(x => x.ToString()).ToArray());

                // Отрисовка графика.
                tensionChart.DrawTension(spokeTensionChart, "Left Side Spokes", leftSpokesAngles, leftSideSpokesTm1);
                tensionChart.DrawTension(spokeTensionChart, "Right Side Spokes", rightSpokesAngles, rightSideSpokesTm1);

                // Расчет стандартного отклонения натяжения спиц.
                standartDevLeftSpokesTensionTextBox.Text = parameterCalculations.StdDev(leftSideSpokesTensionKgf.ToList()).ToString();
                standartDevRightSpokesTensionTextBox.Text = parameterCalculations.StdDev(rightSideSpokesTensionKgf.ToList()).ToString();

                // Расчет среднего натяжения спиц.
                averageLeftSpokesTensionTextBox.Text = leftSideSpokesTensionKgf.Average().ToString();
                averageRightSpokesTensionTextBox.Text = rightSideSpokesTensionKgf.Average().ToString();

                // Допустимое натяжение спиц.
                int variance = varianceTrackBar.Value;

                // Расчет границ натяжения спиц.
                leftSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), variance, true).ToString();

                leftSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), variance, false).ToString();

                rightSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), variance, true).ToString();

                rightSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), variance, false).ToString();

                // Нижняя граница силы натяжения спиц левой стороны колеса.
                var leftSpokesLowerTensionLimit = double.Parse(leftSpokesLowerTensionLimitTextBox.Text);
                // Верхняя граница силы натяжения спиц левой стороны колеса.
                var leftSpokesUpperTensionLimit = double.Parse(leftSpokesUpperTensionLimitTextBox.Text);
                // Нижняя граница силы натяжения спиц правой стороны колеса.
                var rightSpokesLowerTensionLimit = double.Parse(rightSpokesLowerTensionLimitTextBox.Text);
                // Верхняя граница силы натяжения спиц правой стороны колеса.
                var rightSpokesUpperTensionLimit = double.Parse(rightSpokesUpperTensionLimitTextBox.Text);

                // Определение выхода силы натяжения за границы допустимого интервала спиц левой стороны колеса. 
                parameterCalculations.SetWithinTensionLimit(
                    leftSideSpokesGroupBox,
                    withinTensionLimitLeftSpokesLabel,
                    errorProviderTensionLimitError,
                    "leftSideSpokesTensionTextBox",
                    leftSpokesLowerTensionLimit,
                    leftSpokesUpperTensionLimit);

                // Определение выхода силы натяжения за границы допустимого интервала спиц правой стороны колеса.
                parameterCalculations.SetWithinTensionLimit(
                    rightSideSpokesGroupBox,
                    withinTensionLimitRightSpokesLabel,
                    errorProviderTensionLimitError,
                    "rightSideSpokesTensionTextBox",
                    rightSpokesLowerTensionLimit,
                    rightSpokesUpperTensionLimit);
            }
        }

        // Событие открытия окна "About".
        /// <summary>
        /// Событие открытия окна "About".
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Объект About.
            About about = new About();
            // Открытие окна "About".
            about.Show();
        }

        // Событие нажатие на кнопку "Save" в меню программы.
        /// <summary>
        /// Событие нажатие на кнопку "Save" в меню программы.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Настройки диалогового окна сохранения конфигурационного файла.
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Select file to save",
                Filter = "Config files (*.config)|*.config",
                RestoreDirectory = true
            };

            // Если в диалоговом окне нажата кнопка "OK", то сохраняем настройки программы.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Если файл по указанному имени файла существует. то удаляем его.
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                // Путь к конфигурационному файлу, в который будут сохранены настройки программы.
                var appSettingPath = saveFileDialog.FileName;

                // Сохранение настроек программы в конфигурационный файл.
                SaveSettings(appSettingPath);
            }
        }

        // Событие нажатие на кнопку "Open" в меню программы.
        /// <summary>
        /// Событие нажатие на кнопку "Open" в меню программы.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Настройки диалогового окна загрузки конфигурационного файла.
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select file to open",
                Filter = "Config files (*.config)|*.config",
                RestoreDirectory = true
            };

            // Показываем диалоговое окно загрузки конфигцрационного файла.
            openFileDialog.ShowDialog();

            // Путь к конфигурационному файлу, из которого будут загружены настройки в программы.
            var appSettingPath = openFileDialog.FileName;

            // Загрузка настроек программы из конфигурационного файла.
            LoadSettings(appSettingPath);
        }

        // Событие нажатие на кнопку "Exit" в меню программы.
        /// <summary>
        /// Событие нажатие на кнопку "Exit" в меню программы.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Закрытие программы.
            Application.Exit();
        }

        // Событие, происходящее при изменение значения количества спиц левой стороны колеса.
        /// <summary>
        /// Событие, происходящее при изменение значения количества спиц левой стороны колеса.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void leftSideSpokeCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // Получение значение количества спиц левой стороны колеса.
            var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);

            if (!String.IsNullOrEmpty(leftSideSpokeCountComboBoxSelected))
            {
                leftSideSpokeCountComboBox_TextChanged(leftSideSpokeCountComboBox, e);
            }

            SendKeys.SendWait("{ENTER}");
        }

        // Событие, происходящее при изменение значения количества спиц правой стороны колеса.
        /// <summary>
        /// Событие, происходящее при изменение значения количества спиц правой стороны колеса.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void rightSideSpokeCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            // Получение значение количества спиц правой стороны колеса.
            var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

            if (!String.IsNullOrEmpty(rightSideSpokeCountComboBoxSelected))
            {
                rightSideSpokeCountComboBox_TextChanged(rightSideSpokeCountComboBox, e);
            }

            SendKeys.SendWait("{ENTER}");
        }

        // Загрузка настроек программы из конфигурационного файла.
        /// <summary>
        /// Загрузка настроек программы из конфигурационного файла.
        /// </summary>
        /// <param name="appSettingPath">Путь к конфигурационному файлу, из которого будут загружены настройки в программу.</param>
        private void LoadSettings(string appSettingPath)
        {
            // Если путь для сохранения конфигурационного файла не пустой, то сохраняем настройки.
            if (!String.IsNullOrEmpty(appSettingPath))
            {
                // Объект AppSettings.
                var appSettings = new AppSettings(appSettingPath);

                // Словарь настроек в формате ключ - значение.
                var settings = appSettings.LoadSettings();

                try
                {
                    // Массив значений TM-1 Reading спиц левой стороны колеса.
                    var leftSideSpokesTm1ReadingNumericUpDownValues = new List<string>();
                    // Массив значений TM-1 Reading спиц правой стороны колеса.
                    var rightSideSpokesTm1ReadingNumericUpDownValues = new List<string>();

                    // Установка материала спицы.
                    materialComboBox.SelectedItem = settings["materialComboBoxSelectedItem"];
                    // Установка формы спицы.
                    shapeComboBox.SelectedItem = settings["shapeComboBoxSelectedItem"];
                    // Установка толщины спицы.
                    thicknessComboBox.SelectedItem = settings["thicknessComboBoxSelectedItem"];
                    // Установка допустимого отклонения натяжения спицы.
                    varianceTrackBar.Value = int.Parse(settings["varianceTrackBarValue"]);
                    // Установка количества спиц левой стороны колеса.
                    leftSideSpokeCountComboBox.SelectedItem = settings["leftSideSpokeCountComboBoxSelectedItem"];
                    // Установка количества спиц правой стороны колеса.
                    rightSideSpokeCountComboBox.SelectedItem = settings["rightSideSpokeCountComboBoxSelectedItem"];

                    // Если настройка количества спиц левой стороны коелса не пустая, то заполняем массив значений TM-1 Reading.
                    if (!String.IsNullOrEmpty(settings["leftSideSpokeCountComboBoxSelectedItem"]))
                    {
                        for (int i = 0; i < int.Parse(settings["leftSideSpokeCountComboBoxSelectedItem"]); i++)
                        {
                            // Ключ TM-1 Reading левой стороны колеса.
                            var key = $"leftSideSpokesTm1ReadingNumericUpDown{i + 1}";

                            // Если значение по ключу есть, то добавляем значение в массив TM-1 Reading.
                            if (!String.IsNullOrEmpty(settings[key]))
                            {
                                leftSideSpokesTm1ReadingNumericUpDownValues.Add(settings[key]);
                            }
                        }

                        // Заполнение значений TM-1 Reading левой стороны колеса из конфигурационного файла.
                        formControl.SetValuesToGroupControlsText(
                            leftSideSpokesGroupBox,
                            "leftSideSpokesTm1ReadingNumericUpDown",
                            leftSideSpokesTm1ReadingNumericUpDownValues.ToArray());
                    }

                    // Если настройка количества спиц правой стороны коелса не пустая, то заполняем массив значений TM-1 Reading.
                    if (!String.IsNullOrEmpty(settings["rightSideSpokeCountComboBoxSelectedItem"]))
                    {
                        for (int i = 0; i < int.Parse(settings["rightSideSpokeCountComboBoxSelectedItem"]); i++)
                        {
                            // Ключ TM-1 Reading правой стороны колеса.
                            var key = $"rightSideSpokesTm1ReadingNumericUpDown{i + 1}";

                            // Если значение по ключу есть, то добавляем значение в массив TM-1 Reading.
                            if (!String.IsNullOrEmpty(settings[key]))
                            {
                                rightSideSpokesTm1ReadingNumericUpDownValues.Add(settings[key]);
                            }
                        }

                        // Заполнение значений TM-1 Reading правой стороны колеса из конфигурационного файла.
                        formControl.SetValuesToGroupControlsText(
                            rightSideSpokesGroupBox,
                            "rightSideSpokesTm1ReadingNumericUpDown",
                            rightSideSpokesTm1ReadingNumericUpDownValues.ToArray());
                    }

                    // Если количество спиц левой и правой стороны колеса выбраны, то рисуем график.
                    if (!String.IsNullOrEmpty(settings["leftSideSpokeCountComboBoxSelectedItem"]) &&
                        !String.IsNullOrEmpty(settings["rightSideSpokeCountComboBoxSelectedItem"]))
                    {
                        calculateButton.PerformClick();
                    }
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show($"The settings hasn't been loaded!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Сохранение настроек программы в конфигурационный файл.
        /// <summary>
        /// Сохранение настроек программы в конфигурационный файл.
        /// </summary>
        /// <param name="appSettingPath">Путь к конфигурационному файлу, в который будут сохранены настройки программы.</param>
        private void SaveSettings(string appSettingPath)
        {
            // Если путь для сохранения конфигурационного файла не пустой, то сохраняем настройки.
            if (!String.IsNullOrEmpty(appSettingPath))
            {
                // Объект AppSettings.
                var appSettings = new AppSettings(appSettingPath);

                // Словарь настроек в формате ключ - значение.
                var settings = new Dictionary<string, string>();

                // Материал спицы.
                var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
                // Формы спицы.
                var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);
                // Толщина спицы.
                var thicknessComboBoxSelected = thicknessComboBox.GetItemText(thicknessComboBox.SelectedItem);
                // Допустимое отклонение натяжения спиц.
                var varianceTrackBarValue = varianceTrackBar.Value.ToString();
                // Количество спиц левой стороны колеса.
                var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);
                // Количество спиц правой стороны колеса.
                var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

                // Массив значений TM-1 Reading спиц левой стороны колеса.
                List<string> leftSideSpokesTm1ReadingNumericUpDownValues = formControl.GetValuesFromGroupControls(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown").Values.ToList();

                // Массив значений TM-1 Reading спиц правой стороны колеса.
                List<string> rightSideSpokesTm1ReadingNumericUpDownValues = formControl.GetValuesFromGroupControls(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown").Values.ToList();

                // Добавление настроек в словарь.
                settings.Add("materialComboBoxSelectedItem", materialComboBoxSelected);
                settings.Add("shapeComboBoxSelectedItem", shapeComboBoxSelected);
                settings.Add("thicknessComboBoxSelectedItem", thicknessComboBoxSelected);
                settings.Add("varianceTrackBarValue", varianceTrackBarValue);
                settings.Add("leftSideSpokeCountComboBoxSelectedItem", leftSideSpokeCountComboBoxSelected);
                settings.Add("rightSideSpokeCountComboBoxSelectedItem", rightSideSpokeCountComboBoxSelected);

                for (int i = 0; i < leftSideSpokesTm1ReadingNumericUpDownValues.Count; i++)
                {
                    settings.Add($"leftSideSpokesTm1ReadingNumericUpDown{i + 1}", leftSideSpokesTm1ReadingNumericUpDownValues[i]);
                }

                for (int i = 0; i < rightSideSpokesTm1ReadingNumericUpDownValues.Count; i++)
                {
                    settings.Add($"rightSideSpokesTm1ReadingNumericUpDown{i + 1}", rightSideSpokesTm1ReadingNumericUpDownValues[i]);
                }

                // Сохранение настроек в конфигурационный файл из словаря.
                appSettings.SaveSettings(settings);
            }
        }

        // Событие изменения значения допустимого натяжения спиц.
        /// <summary>
        /// Событие изменения значения допустимого натяжения спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void varianceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Допустимое натяжение спиц.
            var varianceTrackBarValue = varianceTrackBar.Value;

            // Изменение допустимого натяжения спиц во всех местах, где оно используется.
            varianceValueLabel.Text = $"{varianceTrackBarValue}%";
            withinTensionLimitLeftSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
            withinTensionLimitRightSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
            leftSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Upper Tension Limit (kgf)";
            rightSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Lower Tension Limit (kgf)";
            leftSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Upper Tension Limit (kgf)";
            rightSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Lower Tension Limit (kgf)";
        }

        // Событие скроллинга TrackBar допустимого натяжения спиц.
        /// <summary>
        /// Событие скроллинга TrackBar допустимого натяжения спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void varianceTrackBar_Scroll(object sender, EventArgs e)
        {
            isTrackbarScrolling = true;
        }

        // Событие отпускания кнопки мыши на TrackBar допустимого натяжения спиц.
        /// <summary>
        /// Событие отпускания кнопки мыши на TrackBar допустимого натяжения спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void varianceTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            // Если кнопка мыши зажата и скроллинг включен, то 
            if (isTrackbarMouseDown == true && isTrackbarScrolling == true)
            {
                // Допустимое натяжение спиц.
                var varianceTrackBarValue = varianceTrackBar.Value;

                // Изменение допустимого натяжения спиц во всех местах, где оно используется.
                withinTensionLimitLeftSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
                withinTensionLimitRightSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
                leftSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Upper Tension Limit (kgf)";
                rightSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Lower Tension Limit (kgf)";
                leftSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Upper Tension Limit (kgf)";
                rightSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Lower Tension Limit (kgf)";

                // Расчет границ натяжения спиц.
                leftSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                        double.Parse(averageLeftSpokesTensionTextBox.Text), varianceTrackBarValue, true).ToString();

                leftSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), varianceTrackBarValue, false).ToString();

                rightSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), varianceTrackBarValue, true).ToString();

                rightSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), varianceTrackBarValue, false).ToString();

                // Нижняя граница силы натяжения спиц левой стороны колеса.
                var leftSpokesLowerTensionLimit = double.Parse(leftSpokesLowerTensionLimitTextBox.Text);
                // Верхняя граница силы натяжения спиц левой стороны колеса.
                var leftSpokesUpperTensionLimit = double.Parse(leftSpokesUpperTensionLimitTextBox.Text);
                // Нижняя граница силы натяжения спиц правой стороны колеса.
                var rightSpokesLowerTensionLimit = double.Parse(rightSpokesLowerTensionLimitTextBox.Text);
                // Верхняя граница силы натяжения спиц правой стороны колеса.
                var rightSpokesUpperTensionLimit = double.Parse(rightSpokesUpperTensionLimitTextBox.Text);

                // Определение выхода силы натяжения за границы допустимого интервала спиц левой стороны колеса. 
                parameterCalculations.SetWithinTensionLimit(
                    leftSideSpokesGroupBox,
                    withinTensionLimitLeftSpokesLabel,
                    errorProviderTensionLimitError,
                    "leftSideSpokesTensionTextBox",
                    leftSpokesLowerTensionLimit,
                    leftSpokesUpperTensionLimit);

                // Определение выхода силы натяжения за границы допустимого интервала спиц правой стороны колеса. 
                parameterCalculations.SetWithinTensionLimit(
                    rightSideSpokesGroupBox,
                    withinTensionLimitRightSpokesLabel,
                    errorProviderTensionLimitError,
                    "rightSideSpokesTensionTextBox",
                    rightSpokesLowerTensionLimit,
                    rightSpokesUpperTensionLimit);
            }

            // Сброс флагов зажатия левой кнопки мыши и скроллинга.
            isTrackbarMouseDown = false;
            isTrackbarScrolling = false;
        }

        // Событие зажатия кнопки мыши на TrackBar - допустимого натяжения спиц.
        /// <summary>
        /// Событие зажатия кнопки мыши на TrackBar - допустимого натяжения спиц.
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void varianceTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            isTrackbarMouseDown = true;
        }

        private void updaterNotifyIcon_Click(object sender, EventArgs e)
        {
            var updaterForm = new UpdaterForm();

            updaterForm.Show();
        }

        private void updaterNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            var updaterForm = new UpdaterForm();

            updaterForm.Show();
        }
    }
}

