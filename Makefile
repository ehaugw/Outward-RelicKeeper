include Makefile.helpers
modname = RelicKeeper
exports = resources/artsource/exports
unityassets = resources/unity/RelicKeeper/Assets
unityassetbundles = resources/unity/RelicKeeper/Assets/AssetBundles

dependencies = BaseDamageModifiers EffectSourceConditions RelicCondition SynchronizedWorldObjects TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname) --no-print-directory
	
	@make basefolders
	
	@make skill NAME="ManaFlow" FILENAME="mana_flow"
	@make skill NAME="RelicFundamentals" FILENAME="relic_fundamentals"
	@make skill NAME="ArcaneInfluence" FILENAME="arcane_influence"
	@make skill NAME="RelicLore" FILENAME="relic_lore"
	@make skill NAME="MythicLore" FILENAME="mythic_lore"
	@make skill NAME="Unleash" FILENAME="unleash"
	@make skill NAME="UseRelic" FILENAME="use_relic"
	@make skill NAME="ManaFlow" FILENAME="mana_flow"
	
	@make item NAME="GildedRelic" FILENAME="gilded_relic"
	@make item NAME="BasicRelic" FILENAME="basic_relic"
	@make item NAME="WoodooCharm" FILENAME="woodoo_charm"
	
	@make assetbundle FILENAME="gilded_relic"
	@make assetbundle FILENAME="basic_relic"
	@make assetbundle FILENAME="woodoo_charm"

unity:
	cp resources/artsource/woodoo_charm.fbx                                         $(unityassets)/woodoo_charm.fbx
	cp $(exports)/woodoo_charm/woodoo_charm_AlbedoTransparency.png    				$(unityassets)/woodoo_charm_AlbedoTransparency.png
	cp $(exports)/woodoo_charm/woodoo_charm_MetallicSmoothness.png    				$(unityassets)/woodoo_charm_MetallicSmoothness.png
	cp $(exports)/woodoo_charm/woodoo_charm_Normal.png                				$(unityassets)/woodoo_charm_Normal.png
	cp resources/artsource/basic_relic.fbx                                          $(unityassets)/basic_relic.fbx
	cp $(exports)/basic_relic/basic_relic_AlbedoTransparency.png    				$(unityassets)/basic_relic_AlbedoTransparency.png
	cp $(exports)/basic_relic/basic_relic_MetallicSmoothness.png    				$(unityassets)/basic_relic_MetallicSmoothness.png
	cp $(exports)/basic_relic/basic_relic_Normal.png                				$(unityassets)/basic_relic_Normal.png
	cp $(exports)/gilded_relic/basic_relic_AlbedoTransparency.png    				$(unityassets)/gilded_relic_AlbedoTransparency.png
	cp $(exports)/gilded_relic/basic_relic_MetallicSmoothness.png    				$(unityassets)/gilded_relic_MetallicSmoothness.png
	cp $(exports)/gilded_relic/basic_relic_Normal.png                				$(unityassets)/gilded_relic_Normal.png

forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
backup:
	mkdir -p ../../OutwardModdingGraphicsBackup/resources/artsource
	mkdir -p ../../OutwardModdingGraphicsBackup/resources/icons
	cp -u resources/artsource/*.blend ../../OutwardModdingGraphicsBackup/resources/artsource
	cp -u resources/artsource/*.spp ../../OutwardModdingGraphicsBackup/resources/artsource
	cp -u resources/icons/*.pdn ../../OutwardModdingGraphicsBackup/resources/icons
