# polpabp-framework

# Develop

Switch to main branch.

# Release

1. Switch to release branch
2. Run
> git merge main
3. Run (cygwin)
> bump version
4. Run (cygwin)
> gulp
5. Run (cygwin)
> make clean
6. Run (powershell)
> make build Config=Release 

# Deploy
1. Run 
> make deploy -f Makefile.deploy NugetSource=xx
