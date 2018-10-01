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

namespace ElectionCalculatorView.ViewModel
{
    public class ResultViewModel : LogoutViewModel
    {
        public ResultViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());
            ExportToPdfCmd = new RelayCommand(x => ExportToPdf());
            ExportToCsvCmd = new RelayCommand(x => ExportToCsv());

            Data = mainViewModel.ResultBusiness.GetResult();
        }

        public ResultViewModel(MainWindowViewModel mainViewModel, Result data) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());

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

            string delimiter = ",";

            StringBuilder sb = new StringBuilder();
            List<string> CsvRow = new List<string>
            {
                "Number of votes",
                "Name",
                "Party"
            };

            sb.AppendLine(string.Join(delimiter, CsvRow));

            foreach (var result in Data.Results)
            {
                CsvRow.Clear();
                CsvRow.Add(result.NumberOfVotes.ToString());
                CsvRow.Add(result.Candidate.Name);
                CsvRow.Add(result.Candidate.Party);

                sb.AppendLine(string.Join(delimiter, CsvRow));
            }

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

            string delimiter = ",";

            StringBuilder sb = new StringBuilder();
            List<string> CsvRow = new List<string>
            {
                "Number of votes",
                "Party"
            };

            sb.AppendLine(string.Join(delimiter, CsvRow));

            foreach (var result in Data.PartiesResults)
            {
                CsvRow.Clear();
                CsvRow.Add(result.NumberOfVotes.ToString());
                CsvRow.Add(result.Party);

                sb.AppendLine(string.Join(delimiter, CsvRow));
            }

            File.WriteAllText(saveFileDialog.FileName, sb.ToString());
        }

        private void ExportToCsv()
        {
            ExportCandidatesResult();

            ExportPartiesResult();
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

                BaseFont bffont = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\ARIALUNI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font fontozel = new Font(bffont, 12);

                var spacer = new Paragraph("")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                };
                pdfDoc.Add(spacer);

                var candidateTable = new PdfPTable(new[] { 1.5f, 2f, 2f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                candidateTable.AddCell("Number of votes");
                candidateTable.AddCell("Name");
                candidateTable.AddCell("Party");

                foreach (CandidateResult result in Data.Results)
                {
                    candidateTable.AddCell(new Phrase(result.NumberOfVotes.ToString(), fontozel));
                    candidateTable.AddCell(new Phrase(result.Candidate.Name, fontozel));
                    candidateTable.AddCell(new Phrase(result.Candidate.Party, fontozel));
                }

                pdfDoc.Add(candidateTable);
                pdfDoc.Add(spacer);

                var partyTable = new PdfPTable(new[] { 1.5f, 2f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                partyTable.AddCell("Number of votes");
                partyTable.AddCell("Party");

                foreach (PartiesResults result in Data.PartiesResults)
                {
                    partyTable.AddCell(new Phrase(result.NumberOfVotes.ToString(), fontozel));
                    partyTable.AddCell(new Phrase(result.Party, fontozel));
                }

                pdfDoc.Add(partyTable);
                pdfDoc.Add(spacer);

                var otherTable = new PdfPTable(new[] { 1.5f, 2f })
                {
                    HorizontalAlignment = 1,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 16f }
                };

                otherTable.AddCell("Number of invalid votes");
                otherTable.AddCell("Number of votes without vote right");

                otherTable.AddCell(Data.NumberOfInvalidVotes.ToString());
                otherTable.AddCell(Data.NumberOfVotesWithoutRight.ToString());

                pdfDoc.Add(otherTable);

                pdfDoc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowGraph()
        {
            mainViewModel.OpenGraphView(Data);
        }
    }
}