OutputDir:=nugets
Projects:=src/PolpAbp.Framework.Application/PolpAbp.Framework.Application.csproj \
src/PolpAbp.Framework.Application.Contracts/PolpAbp.Framework.Application.Contracts.csproj \
src/PolpAbp.Framework.Core/PolpAbp.Framework.Core.csproj \
src/PolpAbp.Framework.Core.Shared/PolpAbp.Framework.Core.Shared.csproj \
src/PolpAbp.Framework.Domain/PolpAbp.Framework.Domain.csproj \
src/PolpAbp.Framework.EntityFrameworkCore/PolpAbp.Framework.EntityFrameworkCore.csproj \
tests/PolpAbp.Framework.Domain.Tests/PolpAbp.Framework.Domain.Tests.csproj \
tests/PolpAbp.Framework.EntityFrameworkCore.Tests/PolpAbp.Framework.EntityFrameworkCore.Tests.csproj \
tests/PolpAbp.Framework.TestBase/PolpAbp.Framework.TestBase.csproj

ProjectNames:=$(basename $(Projects))
Nugets:=$(addsuffix .nupkg,$(ProjectNames))

%.nupkg:
	@echo "Build $@"
ifndef Config
	@echo "Please provide the config with Config=debug or Config=release"
else
ifeq ($(Config),Release)
	@echo "Output release version"
	dotnet pack $(addsuffix .csproj,$(basename $@)) --output $(OutputDir) --configuration Release
else
	@echo "Output debug version"
	dotnet pack $(addsuffix .csproj,$(basename $@)) --include-symbols --output $(OutputDir)
endif
endif


build: $(Nugets)
	@echo "********************************"
	@echo "Build done"
	@echo "********************************"

pre-build:
	@echo "Create nugets dir"
	@mkdir nugets
	@echo "Done"

clean:
	@echo "Delete nugets"
	if [ -d nugets ]; then \
	  rm -rf nugets; \
        fi 
	@echo "Done"

push:
	@echo "Push to GitHub"
	git push

.PHONY: build pre-build clean push
