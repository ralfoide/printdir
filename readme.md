PrintDirXP is small C# windows form app to select a directory and generate a print out of its listing.

It is the winform equivalent of running "dir > prn" in a cmd/dos box but with a point-and-click interface.

The display is in French, because that's the requirement for the one user of this small utility.

Selecting a folder in C# is done using 2 extensions: SHBrowser worked well under Windows XP/7 32-bit,
whereas FolderBrowser works well under Windows 7/8 64-bit. Both sources include their source
reference and license.

This utility is provided under a MIT License. Reuse is encouraged. Attribution is deemed polite but not mandatory.
