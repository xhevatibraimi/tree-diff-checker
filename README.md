# Folder Tree Diff Checker

## description

This project represents a cli tool that can compare 2 folders, in a way that it prints out the files that are added/modified/deleted/untouched.

## Structure: It has 2 comparers:

- eager comparer (it compares 2 folders by loading all paths of the folders at once, and then comparing them 
- lazy comparer (it compares 2 folders by iterating the paths one by one, on demand)
