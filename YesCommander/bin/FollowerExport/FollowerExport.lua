﻿local frame = CreateFrame("FRAME"); -- Need a frame to respond to events
frame:RegisterEvent("ADDON_LOADED"); -- Fired when saved variables are loaded
frame:RegisterEvent("PLAYER_LOGOUT"); -- Fired when about to log out
function frame:OnEvent(event, arg1)
	if event == "ADDON_LOADED" and arg1 == "FollowerExport" then
		if DisplayID_Bool == nil then
			print("检测到新帐号登录");
			DisplayID_Bool=false;
		end
		--DisplayID:SetChecked(DisplayID_Bool);

		if FollowerString == nil then
			print("检测到新人物登录，"..UnitName("player"));
			--print("随从数据文件路径：".."WTF/Account/帐号名/"..GetRealmName().."/"..UnitName("player").."/FollowerExport.lua");
		else
			--print(UnitName("player").."的随从数据文件已经存在，将在退出游戏时自动更新");
		end
	elseif event == "PLAYER_LOGOUT" then
		SavedFollowerString = (FollowerString):gsub("|n","\n");
	end
end
frame:SetScript("OnEvent", frame.OnEvent);
SLASH_FOLLOWEREXPORT1, SLASH_FOLLOWEREXPORT2 = '/followerexport', '/fe'; -- 3.
ExpTable={
	400,
	800,
	1200,
	1600,
	2000,
	3000,
	3500,
	4000,
	5400,
	6000,
	60000,
	120000
};

--FollowerString="";
function SlashCmdList.FOLLOWEREXPORT(msg, editbox) -- 4.
	GetFollowerString();
	FollowExportFrame:Show();
	--print(SavedFollowerString);
end

local raceAlliance={[466]="德莱尼",[225]="兽人",[177]="人类",[178]="人类",[203]="豺狼人",[455]="侏儒",[406]="狼人",[382]="矮人",[351]="矮人",[285]="德莱尼",[363]="人类",[237]="暗夜精灵",[429]="矮人",[280]="狼人",[279]="矮人",[417]="人类",[364]="暗夜精灵",[400]="人类",[345]="人类",[459]="德莱尼",[394]="矮人",[297]="暗夜精灵",[462]="高等鸦人",[207]="德莱尼",[333]="矮人",[216]="矮人",[387]="矮人",[250]="狼人",[424]="狼人",[357]="暗夜精灵",[298]="熊猫人",[231]="人类",[388]="矮人",[327]="德莱尼",[430]="暗夜精灵",[465]="人类",[334]="矮人",[346]="暗夜精灵",[255]="暗夜精灵",[274]="暗夜精灵",[412]="人类",[370]="狼人",[260]="暗夜精灵",[376]="侏儒",[273]="人类",[286]="人类",[219]="刃牙虎人",[352]="狼人",[244]="暗夜精灵",[375]="矮人",[238]="狼人",[445]="熊猫人",[399]="人类",[439]="人类",[405]="矮人",[292]="暗夜精灵",[202]="人类",[393]="德莱尼",[340]="矮人",[303]="矮人",[328]="人类",[440]="暗夜精灵",[267]="狼人",[194]="木精",[460]="地精",[232]="暗夜精灵",[411]="侏儒",[304]="暗夜精灵",[358]="暗夜精灵",[452]="侏儒",[381]="德莱尼",[249]="暗夜精灵",[224]="鸦人流亡者",[218]="鸦人流亡者",[423]="暗夜精灵",[291]="人类",[266]="暗夜精灵",[446]="人类",[193]="食人魔",[451]="侏儒",[243]="矮人",[261]="狼人",[256]="狼人",[458]="德莱尼",[339]="德莱尼",[418]="暗夜精灵",[369]="暗夜精灵",[342]="人类",[209]="巨魔",[227]="人类",[204]="人类",[208]="德莱尼",[91]="德莱尼",[395]="人类",[184]="德莱尼",[92]="人类",[341]="人类",[347]="人类",[179]="德莱尼",[87]="人类",[287]="人类",[390]="德莱尼",[107]="德莱尼",[326]="矮人",[235]="德莱尼",[288]="侏儒",[189]="独眼魔",[230]="狼人",[447]="矮人",[444]="侏儒",[371]="矮人",[153]="矮人",[254]="狼人",[365]="侏儒",[295]="血精灵",[408]="狼人",[271]="矮人",[262]="暗夜精灵",[401]="矮人",[360]="狼人",[434]="锦鱼人",[385]="矮人",[368]="人类",[377]="矮人",[32]="食人魔",[463]="暗夜精灵",[276]="人类",[413]="人类",[258]="狼人",[265]="狼人",[96]="暗夜精灵",[427]="侏儒",[431]="矮人",[350]="侏儒",[301]="矮人",[379]="矮人",[354]="暗夜精灵",[115]="矮人",[441]="矮人",[410]="狼人",[398]="人类",[323]="矮人",[277]="德莱尼",[289]="矮人",[450]="暗夜精灵",[335]="德莱尼",[426]="矮人",[467]="熊猫人",[384]="矮人",[93]="侏儒",[180]="狼人",[89]="矮人",[436]="熊猫人",[299]="德莱尼",[270]="狼人",[330]="人类",[383]="德莱尼",[397]="矮人",[88]="侏儒",[211]="矮人",[170]="刃牙虎人",[245]="暗夜精灵",[113]="矮人",[110]="矮人",[449]="暗夜精灵",[119]="熊猫人",[294]="狼人",[403]="矮人",[236]="矮人",[343]="矮人",[453]="矮人",[402]="侏儒",[263]="暗夜精灵",[331]="德莱尼",[248]="狼人",[190]="人类",[97]="矮人",[355]="矮人",[419]="矮人",[396]="人类",[374]="暗夜精灵",[105]="人类",[359]="人类",[253]="暗夜精灵",[386]="熊猫人",[416]="暗夜精灵",[432]="熊猫人",[112]="暗夜精灵",[414]="狼人",[284]="暗夜精灵",[407]="侏儒",[392]="德莱尼",[283]="人类",[448]="猢狲",[247]="暗夜精灵",[361]="暗夜精灵",[192]="地精",[348]="侏儒",[421]="德莱尼",[442]="侏儒",[269]="人类",[373]="人类",[102]="人类",[157]="兽人",[422]="人类",[391]="德莱尼",[356]="狼人",[118]="熊猫人",[332]="德莱尼",[437]="熊猫人",[338]="德莱尼",[362]="侏儒",[106]="人类",[103]="人类",[272]="暗夜精灵",[438]="矮人",[154]="人类",[239]="德莱尼",[329]="人类",[372]="矮人",[114]="德莱尼",[302]="人类",[94]="人类",[233]="侏儒",[155]="德莱尼",[443]="德莱尼",[228]="狼人",[117]="人类",[415]="矮人",[409]="狼人",[337]="人类",[95]="暗夜精灵",[290]="暗夜精灵",[275]="矮人",[349]="德莱尼",[109]="德莱尼",[98]="暗夜精灵",[111]="矮人",[378]="矮人",[325]="矮人",[366]="熊猫人",[242]="人类",[176]="德莱尼",[171]="机械",[34]="暗夜精灵",[264]="暗夜精灵",[252]="狼人",[185]="德莱尼",[212]="德莱尼",[159]="德莱尼",[324]="人类",[300]="人类",[99]="狼人",[101]="暗夜精灵",[380]="矮人",[183]="兽人",[246]="狼人",[404]="侏儒",[259]="狼人",[435]="人类",[268]="暗夜精灵",[90]="侏儒",[251]="狼人",[428]="人类",[296]="矮人",[234]="侏儒",[182]="人类",[425]="德莱尼",[293]="侏儒",[344]="熊猫人",[172]="人类",[205]="德莱尼",[281]="矮人",[120]="熊猫人",[420]="熊猫人",[116]="暗夜精灵",[108]="狼人",[367]="侏儒",[241]="矮人",[336]="人类",[217]="暗夜精灵",[229]="矮人",[282]="熊猫人",[353]="人类",[278]="矮人",[186]="德莱尼",[100]="狼人",[195]="人类",[104]="狼人",[257]="暗夜精灵",[433]="熊猫人",[240]="人类",[389]="德莱尼",[168]="埃匹希斯守卫",[468]="兽人",[474]="兽人"}
local raceHorde={[466]="兽人",[225]="兽人",[177]="人类",[178]="人类",[203]="豺狼人",[455]="侏儒",[406]="地精",[382]="兽人",[351]="巨魔",[285]="兽人",[363]="兽人",[237]="兽人",[429]="熊猫人",[280]="地精",[279]="巨魔",[417]="兽人",[364]="兽人",[400]="兽人",[345]="被遗忘者",[459]="兽人",[394]="兽人",[297]="巨魔",[462]="高等鸦人",[207]="血精灵",[333]="牛头人",[216]="被遗忘者",[387]="地精",[250]="牛头人",[424]="地精",[357]="地精",[298]="地精",[231]="血精灵",[388]="牛头人",[327]="血精灵",[430]="牛头人",[465]="人类",[334]="牛头人",[346]="巨魔",[255]="巨魔",[274]="牛头人",[412]="血精灵",[370]="地精",[260]="牛头人",[376]="血精灵",[273]="兽人",[286]="牛头人",[219]="刃牙虎人",[352]="被遗忘者",[244]="牛头人",[375]="巨魔",[238]="地精",[445]="兽人",[399]="兽人",[439]="兽人",[405]="地精",[292]="兽人",[202]="人类",[393]="兽人",[340]="牛头人",[303]="地精",[328]="牛头人",[440]="熊猫人",[267]="巨魔",[194]="木精",[460]="地精",[232]="牛头人",[411]="巨魔",[304]="血精灵",[358]="血精灵",[452]="巨魔",[381]="牛头人",[249]="牛头人",[224]="鸦人流亡者",[218]="鸦人流亡者",[423]="兽人",[291]="兽人",[266]="牛头人",[446]="牛头人",[193]="食人魔",[451]="熊猫人",[243]="巨魔",[261]="牛头人",[256]="巨魔",[458]="兽人",[339]="血精灵",[418]="牛头人",[369]="地精",[342]="人类",[209]="巨魔",[227]="兽人",[204]="被遗忘者",[208]="德莱尼",[91]="被遗忘者",[395]="血精灵",[184]="兽人",[92]="兽人",[341]="被遗忘者",[347]="被遗忘者",[179]="兽人",[87]="兽人",[287]="兽人",[390]="巨魔",[107]="血精灵",[326]="牛头人",[235]="血精灵",[288]="被遗忘者",[189]="独眼魔",[230]="兽人",[447]="被遗忘者",[444]="巨魔",[371]="被遗忘者",[153]="兽人",[254]="巨魔",[365]="兽人",[295]="血精灵",[408]="被遗忘者",[271]="被遗忘者",[262]="牛头人",[401]="兽人",[360]="血精灵",[434]="锦鱼人",[385]="巨魔",[368]="血精灵",[377]="兽人",[32]="食人魔",[463]="被遗忘者",[276]="巨魔",[413]="兽人",[258]="牛头人",[265]="巨魔",[96]="巨魔",[427]="牛头人",[431]="兽人",[350]="牛头人",[301]="巨魔",[379]="熊猫人",[354]="被遗忘者",[115]="血精灵",[441]="牛头人",[410]="被遗忘者",[398]="血精灵",[323]="牛头人",[277]="血精灵",[289]="被遗忘者",[450]="被遗忘者",[335]="血精灵",[426]="兽人",[467]="熊猫人",[384]="牛头人",[93]="巨魔",[180]="巨魔",[89]="地精",[436]="熊猫人",[299]="血精灵",[270]="被遗忘者",[330]="血精灵",[383]="巨魔",[397]="被遗忘者",[88]="兽人",[211]="地精",[170]="刃牙虎人",[245]="牛头人",[113]="地精",[110]="牛头人",[449]="熊猫人",[119]="熊猫人",[294]="巨魔",[403]="血精灵",[236]="牛头人",[343]="牛头人",[453]="被遗忘者",[402]="巨魔",[263]="巨魔",[331]="血精灵",[248]="巨魔",[190]="人类",[97]="被遗忘者",[355]="巨魔",[419]="被遗忘者",[396]="兽人",[374]="巨魔",[105]="血精灵",[359]="兽人",[253]="牛头人",[386]="兽人",[416]="地精",[432]="熊猫人",[112]="血精灵",[414]="血精灵",[284]="被遗忘者",[407]="被遗忘者",[392]="地精",[283]="血精灵",[448]="猢狲",[247]="巨魔",[361]="血精灵",[192]="地精",[348]="血精灵",[421]="血精灵",[442]="巨魔",[269]="兽人",[373]="被遗忘者",[102]="被遗忘者",[157]="兽人",[422]="牛头人",[391]="地精",[356]="血精灵",[118]="熊猫人",[332]="牛头人",[437]="熊猫人",[338]="血精灵",[362]="被遗忘者",[106]="血精灵",[103]="被遗忘者",[272]="熊猫人",[438]="被遗忘者",[154]="血精灵",[239]="熊猫人",[329]="牛头人",[372]="被遗忘者",[114]="被遗忘者",[302]="血精灵",[94]="被遗忘者",[233]="牛头人",[155]="兽人",[443]="血精灵",[228]="地精",[117]="被遗忘者",[415]="被遗忘者",[409]="巨魔",[337]="血精灵",[95]="地精",[290]="熊猫人",[275]="牛头人",[349]="熊猫人",[109]="兽人",[98]="牛头人",[111]="兽人",[378]="牛头人",[325]="牛头人",[366]="巨魔",[242]="被遗忘者",[176]="兽人",[171]="机械",[34]="牛头人",[264]="牛头人",[252]="巨魔",[185]="兽人",[212]="兽人",[159]="兽人",[324]="牛头人",[300]="被遗忘者",[99]="巨魔",[101]="巨魔",[380]="巨魔",[183]="兽人",[246]="巨魔",[404]="巨魔",[259]="巨魔",[435]="兽人",[268]="牛头人",[90]="血精灵",[251]="巨魔",[428]="被遗忘者",[296]="兽人",[234]="巨魔",[182]="兽人",[425]="被遗忘者",[293]="巨魔",[344]="巨魔",[172]="人类",[205]="德莱尼",[281]="兽人",[120]="熊猫人",[420]="巨魔",[116]="血精灵",[108]="巨魔",[367]="熊猫人",[241]="被遗忘者",[336]="血精灵",[217]="牛头人",[229]="被遗忘者",[282]="地精",[353]="牛头人",[278]="牛头人",[186]="兽人",[100]="牛头人",[195]="被遗忘者",[104]="血精灵",[257]="牛头人",[433]="熊猫人",[240]="兽人",[389]="地精",[168]="埃匹希斯守卫",[468]="兽人",[474]="兽人"}

function GetFollowerString()
	FollowerString = "ID,姓名,种族,职业ID,职业,品质,等级,装等,激活,技能ID1,技能1,技能ID2,技能2,特长ID1,特长1,特长ID2,特长2,特长ID3,特长3,升紫经验,升蓝经验,满级经验".."|n";
	local followersList = C_Garrison.GetFollowers();
	race = ((UnitFactionGroup("player"))=="Alliance") and raceAlliance or raceHorde
	for i = 1, #followersList do
		if(followersList[i].isCollected and race[tonumber(followersList[i].garrFollowerID, 16)] ~= nil) then
			id = tonumber(followersList[i].garrFollowerID, 16);
			local str;
			str=id..","..followersList[i].name;
			str=str..","..(race[id] or "船舰No."..id);
			str=str..","..followersList[i].classSpec;
			str=str..","..followersList[i].className;
			str=str..","..followersList[i].quality;
			str=str..","..followersList[i].level;
			str=str..","..(followersList[i].iLevel or "");
			if followersList[i].status~=GARRISON_FOLLOWER_INACTIVE then
				str=str..",1";
			else
				str=str..",0";
			end
			local followerAbilities = C_Garrison.GetFollowerAbilities(followersList[i].followerID)
			local size=0;
			for a = 1, #followerAbilities do
				local ability = followerAbilities[a];
				if ( not ability.isTrait ) then
					for id, counter in pairs(ability.counters) do
						str=str..",";
						str=str..id;
						str=str..","..counter.name;
						size=size+1;
					end
				end
			end
			while size<2 do
				str=str..",";
				str=str..",";
				size=size+1;
			end
			size=0;
			for a = 1, #followerAbilities do
				local ability = followerAbilities[a];
				if (ability.isTrait ) then
					str=str..","..ability.id;
					str=str..","..string.gsub(ability.name,'\r\n','');
					size=size+1;
				end
			end
			while size<3 do
				str=str..",";
				str=str..",";
				size=size+1;
			end
			local xp1=followersList[i].levelXP-followersList[i].xp;
			local lv,qual;
			if (followersList[i].level<100) then
				lv=followersList[i].level+1;
				qual=followersList[i].quality;
			else
				lv=followersList[i].level;
				qual=followersList[i].quality+1;
			end
			while lv<100 do
				xp1=xp1+ExpTable[lv-90];
				lv=lv+1;
			end
			local xp2=xp1;
			if (qual<3) then
				xp2=xp2+ExpTable[11];
			end
			local xp3=xp2;
			if (qual<4) then
				xp3=xp3+ExpTable[12];
			end
			if (followersList[i].level==100) then
				xp1=0;
			end
			if (followersList[i].quality>=3) then
				xp2=0;
			end
			str=str..","..xp3..","..xp2..","..xp1
			FollowerString=FollowerString..str.."|n";
		end
	end
	parentEditBox:SetText(FollowerString);
end
function ExportFrame_OnLoad(self)
	GetFollowerString();
	FollowExportFrame:Hide();
end
function DisplayID_Changed()
	DisplayID_Bool=DisplayID:GetChecked();
	GetFollowerString()
	--print(DisplayID_Bool);
end
