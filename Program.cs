using System;
using System.Diagnostics;
using System.IO;


namespace dotnetCore_pcl_to_pdf
{
    class Program
    {
        static bool _debug = true;

        // need to extract .\reference\GhostPCL\ghostpcl-9.56.1-win64.zip and update path here
        // ghostpcl-9.56.1-win64.zip from: https://ghostscript.com/releases/gpcldnld.html
        static string _ghostscriptPath = @"C:\Apps\GhostPCL\ghostpcl-9.56.1-win64\gpcl6win64.exe";

        // options ordered per: https://www.ghostscript.com/doc/current/Use.htm
        // -dSAFER defaults to active in Ghostscript 9.50+ | -dPAPERSIZE defaults to 'letter'
        static string _ghostscriptArgumentsBaseline = "-sDEVICE=pdfwrite -sOutputFile=\"***fileOut***.pdf\" -dBATCH -dNOPAUSE \"***fileIn***\"";

        // PCL files created from TIFF files via: https://www.vertopal.com/en/convert/tiff-to-pcl
        // TIFF files from: https://people.math.sc.edu/Burkardt/data/tif/tif.html
        static string[] _sourceFiles = new string[] { "at3_1m4_01.pcl", "at3 1m4 01.pcl", "circuit.pcl", "foliage.pcl", "m83.pcl", "moon.pcl", "saturn.pcl" };
        static string _sourceFilesPath = @"C:\Prototypes\dotnetCore_pcl_to_pdf\source\";
        static string _outputFilesPath = @"C:\Prototypes\dotnetCore_pcl_to_pdf\output\";


        static void Main(string[] args)
        {
            bool conversionResult = ConvertFiles(_sourceFiles);

            if (_debug)
            {
                Console.WriteLine("============================================================");
                Console.WriteLine($"Final cumulative conversion result: {conversionResult}");
            }
        }


        static bool ConvertFiles(string[] sourceFiles)
        {
            bool conversionResult = false;
            int cumulativeExitCodes = 0;
            int exitCode = -111;
            string ghostscriptArguments = "";

            if (_debug)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("ConvertFiles() called...");
                Console.WriteLine($"sourceFiles.Length: {sourceFiles.Length}");
                Console.WriteLine($"sourceFiles: {String.Join(", ", sourceFiles)}");
            }

            foreach (string sourceFile in sourceFiles)
            {
                ghostscriptArguments = PopulateGhostscriptArguments(_ghostscriptArgumentsBaseline, sourceFile);
                exitCode = ConvertPclToPdf(ghostscriptArguments);
                cumulativeExitCodes += exitCode;
                exitCode = -111;
            }

            if (cumulativeExitCodes == 0)
            {
                conversionResult = true;
            }

            if (_debug)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine($"conversionResult: {conversionResult}");
            }

            return conversionResult;
        }


        static string PopulateGhostscriptArguments(string ghostscriptArgumentsBaseline, string sourceFile)
        {
            string ghostscriptArguments = "";

            if (_debug)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("PopulateGhostscriptArguments() called...");
                Console.WriteLine($"ghostscriptArgumentsBaseline: {ghostscriptArgumentsBaseline}");
                Console.WriteLine($"sourceFile: {sourceFile}");
            }

            string sourceFileRoot = Path.GetFileNameWithoutExtension(sourceFile);

            ghostscriptArguments = ghostscriptArgumentsBaseline.Replace("***fileIn***", _sourceFilesPath + sourceFile);
            ghostscriptArguments = ghostscriptArguments.Replace("***fileOut***", _outputFilesPath + sourceFileRoot);

            if (_debug)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine($"ghostscriptArguments: {ghostscriptArguments}");
            }

            return ghostscriptArguments;
        }


        static int ConvertPclToPdf(string ghostscriptArguments)
        {
            int exitCode = -1;

            if (_debug)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("ConvertPclToPdf() called...");
                Console.WriteLine($"ghostscriptArguments: {ghostscriptArguments}");
            }

            try
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = _ghostscriptPath;
                    proc.StartInfo.Arguments = ghostscriptArguments;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    proc.Start();
                    proc.WaitForExit();

                    exitCode = proc.ExitCode;
                }

                Console.WriteLine($"Ghostscript process exited - exitCode: {exitCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.Message: {ex.Message}");
                Console.WriteLine($"ex.InnerException: {ex.InnerException}");
                Console.WriteLine($"ex: {ex}");
            }

            if (_debug)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine($"final exitCode: {exitCode}");
            }

            return exitCode;
        }

    }
}