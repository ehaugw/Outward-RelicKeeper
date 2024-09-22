modname = RelicKeeper
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader
exports = resources/artsource/exports
unityassets = resources/unity/RelicKeeper/Assets
unityassetbundles = resources/unity/RelicKeeper/Assets/AssetBundles

dependencies = EffectSourceConditions HolyDamageManager SynchronizedWorldObjects TinyHelper RelicCondition BaseDamageModifiers

assemble:
	# common for all mods
	mkdir -p public/$(pluginpath)/$(modname)
	cp -u bin/$(modname).dll public/$(pluginpath)/$(modname)/
	for dependency in $(dependencies) ; do \
		cp -u ../$${dependency}/bin/$${dependency}.dll public/$(pluginpath)/$(modname)/ ; \
	done
	
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/BasicRelic/Textures/
	cp -u resources/icons/basic_relic.png                      public/$(sideloaderpath)/Items/BasicRelic/Textures/icon.png
	mkdir -p public/$(sideloaderpath)/Items/WoodooCharm/Textures/
	cp -u resources/icons/woodoo_charm.png                      public/$(sideloaderpath)/Items/WoodooCharm/Textures/icon.png
	mkdir -p public/$(sideloaderpath)/Items/GoldLichTalisman/Textures/
	cp -u resources/icons/basic_relic.png                      public/$(sideloaderpath)/Items/GoldLichTalisman/Textures/icon.png
	
	cp -u $(unityassetbundles)/basic_relic                                             public/$(sideloaderpath)/AssetBundles/basic_relic
	cp -u $(unityassetbundles)/woodoo_charm                                            public/$(sideloaderpath)/AssetBundles/woodoo_charm

unity:
	cp resources/artsource/woodoo_charm.fbx                                         $(unityassets)/woodoo_charm.fbx
	cp $(exports)/woodoo_charm/woodoo_charm_AlbedoTransparency.png    				$(unityassets)/woodoo_charm_AlbedoTransparency.png
	cp $(exports)/woodoo_charm/woodoo_charm_MetallicSmoothness.png    				$(unityassets)/woodoo_charm_MetallicSmoothness.png
	cp $(exports)/woodoo_charm/woodoo_charm_Normal.png                				$(unityassets)/woodoo_charm_Normal.png
	cp resources/artsource/basic_relic.fbx                                          $(unityassets)/basic_relic.fbx
	cp $(exports)/basic_relic/basic_relic_AlbedoTransparency.png    				$(unityassets)/basic_relic_AlbedoTransparency.png
	cp $(exports)/basic_relic/basic_relic_MetallicSmoothness.png    				$(unityassets)/basic_relic_MetallicSmoothness.png
	cp $(exports)/basic_relic/basic_relic_Normal.png                				$(unityassets)/basic_relic_Normal.png

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
	if [ ! -f omit.txt ]; then make forceinstall; fi

forceinstall:
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
backup:
	mkdir -p ../../OutwardModdingGraphicsBackup/resources/artsource
	mkdir -p ../../OutwardModdingGraphicsBackup/resources/icons
	cp -u resources/artsource/*.blend ../../OutwardModdingGraphicsBackup/resources/artsource
	cp -u resources/artsource/*.spp ../../OutwardModdingGraphicsBackup/resources/artsource
	cp -u resources/icons/*.pdn ../../OutwardModdingGraphicsBackup/resources/icons
