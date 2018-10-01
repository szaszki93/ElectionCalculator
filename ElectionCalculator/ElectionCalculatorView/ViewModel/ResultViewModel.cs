using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;
using System;
using System.IO;
using System.Windows.Input;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using Microsoft.Win32;
using System.Text;
using System.Collections.Generic;
using ElectionCalculatorView.Resource;

namespace ElectionCalculatorView.ViewModel
{
    public class ResultViewModel : LogoutViewModel
    {
        public const string DELIMITER = ",";

        public ResultViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());
            ExportToPdfCmd = new RelayCommand(x => ExportToPdf());
            ExportToCsvCmd = new RelayCommand(x => ExportToCsv());

            Data = mainViewModel.ResultService.GetResult();
        }

        public ResultViewModel(MainWindowViewModel mainViewModel, Result data) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());
            ExportToPdfCmd = new RelayCommand(x => ExportToPdf());
            ExportToCsvCmd = new RelayCommand(x => ExportToCsv());

            Data = data;
        }

        public Result Data { get; private set; }

        public ICommand ExportToCsvCmd { get; set; }

        public ICommand ExportToPdfCmd { get; set; }

        public ICommand ShowGraphCmd { get; set; }

        private void ExportCandidatesResult()
        {
            string defaultFileName = "CandidatesResults.csv";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                AddExtension = true,
                Filter = "CSV|*.csv"
            };

            if (saveFileDialog.ShowDialog() != true) { return; }

            StringBuilder sb = new StringBuilder();
            List<string> CsvRow = new List<string>
            {
                Language.NumberOfVotes,
                Language.Name,
                Language.Party
            };

            sb.AppendLine(string.Join(DELIMITER, CsvRow));

            foreach (var result in Data.Results)
            {
                CsvRow.Clear();
                CsvRow.Add(result.NumberOfVotes.ToString());
                CsvRow.Add(result.Candidate.Name);
                CsvRow.Add(result.Candidate.Party);

                sb.AppendLine(string.Join(DELIMITER, CsvRow));
            }

            File.WriteAllText(saveFileDialog.FileName, sb.ToString());
        }

        private void ExportOverallResult()
        {
            string defaultFileName = "OverrallResults.csv";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                AddExtension = true,
                Filter = "CSV|*.csv"
            };

            if (saveFileDialog.ShowDialog() != true) { return; }

            StringBuilder sb = new StringBuilder();
            List<string> CsvRow = new List<string>
            {
                Language.NumberOfValidVotes,
                Data.NumberOfValidVotes.ToString(),
            };
            sb.AppendLine(string.Join(DELIMITER, CsvRow));

            CsvRow.Clear();
            CsvRow.Add(Language.NumberOfInvalidVotes);
            CsvRow.Add(Data.NumberOfInvalidVotes.ToString());
            sb.AppendLine(string.Join(DELIMITER, CsvRow));

            CsvRow.Clear();
            CsvRow.Add(Language.NumberOfVotesWithoutRight);
            CsvRow.Add(Data.NumberOfVotesWithoutRight.ToString());
            sb.AppendLine(string.Join(DELIMITER, CsvRow));

            File.WriteAllText(saveFileDialog.FileName, sb.ToString());
        }

        private void ExportPartiesResult()
        {
            string defaultFileName = "PartiesResults.csv";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                AddExtension = true,
                Filter = "CSV|*.csv"
            };

            if (saveFileDialog.ShowDialog() != true) { return; }

            StringBuilder sb = new StringBuilder();
            List<string> CsvRow = new List<string>
            {
                Language.NumberOfVotes,
                Language.Party,
            };

            sb.AppendLine(string.Join(DELIMITER, CsvRow));

            foreach (var result in Data.PartiesResults)
            {
                CsvRow.Clear();
                CsvRow.Add(result.NumberOfVotes.ToString());
                CsvRow.Add(result.Party);

                sb.AppendLine(string.Join(DELIMITER, CsvRow));
            }

            File.WriteAllText(saveFileDialog.FileName, sb.ToString());
        }

        private void ExportToCsv()
        {
            try
            {
                ExportCandidatesResult();
                ExportPartiesResult();
                ExportOverallResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Language.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToPdf()
        {
            try
            {
                string defaultFileName = "Results.pdf";

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = defaultFileName,
                    AddExtension = true,
                    Filter = "Pdf Files|*.pdf"
                };

                if (saveFileDialog.ShowDialog() != true) { return; }

                var pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                pdfDoc.Open();

                string fontPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Font\\ARIALUNI.TTF";

                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 12);

                var candidatesParagraph = new Paragraph(Language.CandidatesResults)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };
                pdfDoc.Add(candidatesParagraph);

                var candidateTable = new PdfPTable(new[] { 1.5f, 2f, 2f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                candidateTable.AddCell(Language.NumberOfVotes);
                candidateTable.AddCell(Language.Name);
                candidateTable.AddCell(Language.Party);

                foreach (CandidateResult result in Data.Results)
                {
                    candidateTable.AddCell(new Phrase(result.NumberOfVotes.ToString(), font));
                    candidateTable.AddCell(new Phrase(result.Candidate.Name, font));
                    candidateTable.AddCell(new Phrase(result.Candidate.Party, font));
                }

                pdfDoc.Add(candidateTable);

                var partiesParagraph = new Paragraph(Language.PartiesResults)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };
                pdfDoc.Add(partiesParagraph);

                var partyTable = new PdfPTable(new[] { 1.5f, 2f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                partyTable.AddCell(Language.NumberOfVotes);
                partyTable.AddCell(Language.Party);

                foreach (PartiesResults result in Data.PartiesResults)
                {
                    partyTable.AddCell(new Phrase(result.NumberOfVotes.ToString(), font));
                    partyTable.AddCell(new Phrase(result.Party, font));
                }

                pdfDoc.Add(partyTable);

                var overrallParagraph = new Paragraph(Language.OverrallResults)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };
                pdfDoc.Add(partiesParagraph);

                var otherTable = new PdfPTable(new[] { 2f, 1.5f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                otherTable.AddCell(Language.NumberOfValidVotes);
                otherTable.AddCell(Data.NumberOfValidVotes.ToString());

                otherTable.AddCell(Language.NumberOfInvalidVotes);
                otherTable.AddCell(Data.NumberOfInvalidVotes.ToString());

                otherTable.AddCell(Language.NumberOfVotesWithoutRight);
                otherTable.AddCell(Data.NumberOfVotesWithoutRight.ToString());

                pdfDoc.Add(otherTable);

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Language.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowGraph()
        {
            mainViewModel.OpenGraphView(Data);
        }
    }
}