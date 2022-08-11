dotnetCore_pcl_to_pdf

Written in C# (.NET 6.0), leverages GhostPCL to convert a batch of PCL files to PDF.


USAGE

1. Install GhostPCL (https://ghostscript.com/releases/gpcldnld.html)
2. Update _ghostscriptPath to specify the appropriate path & executable names for GhostPCL
3. Update _sourceFilesPath to specify the path to the PCL file(s) that you want to convert
4. Update _sourceFiles so that it contains the name of the PCL file(s) that you want to convert
5. Update _outputFilesPath to specify the path the PDF files should be created in
6. Update line 5 in ./dotnetCore_pcl_to_pdf.bat to CD into this app's directory
7. Double-click ./dotnetCore_pcl_to_pdf.bat to run the app and convert your PCL file(s)


LICENSE

dotnetCore_pcl_to_pdf is distributed under the GNU Affero General Public License (see COPYING file)