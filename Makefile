modname = RelicKeeper
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader
unityassets = resources/unity/Particles/Assets/AssetBundles

dependencies = CustomWeaponBehaviour EffectSourceConditions HolyDamageManager SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	mkdir -p public/$(pluginpath)/$(modname)
	cp -u bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp -u ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	

publish:
	make clean
	make assemble
	rm -f $(modname).rar
	rar a $(modname).rar -ep1 public/*
	
	# (cd ../Descriptions && python3 crusader.py)
	
	cp -u resources/manifest.json public/BepInEx/
	cp -u resources/README.md public/BepInEx/
	cp -u resources/icon.png public/BepInEx/
	(cd public/BepInEx && zip -r $(modname)_thunderstore.zip * && mv $(modname)_thunderstore.zip ../../)

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f $(modname).rar
	rm -f $(modname).zip
info:
	echo Modname: $(modname)
play:
	(make install && cd .. && make play)
