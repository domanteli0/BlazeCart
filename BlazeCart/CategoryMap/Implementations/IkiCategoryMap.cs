﻿using System;
using Models;
using Common;
using CategoryMap;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public class IkiCategoryMap : ACategoryMap, ICategoryMap
	{
		public IkiCategoryMap(ILogger logger) : base(logger) { }

		public void Map(List<Category> root_cat, IDictionary<string, Category> into)
		{
            this.addMapper("Cukrus ir druska", new()
            {
                ("(?i)cukrus", into["Cukrus, saldikliai ir druska"]),
                ("(?i)cukraus", into["Cukrus, saldikliai ir druska"]),
                ("(?i)saldiklis", into["Cukrus, saldikliai ir druska"]),
                ("(?i)fruktozė", into["Cukrus, saldikliai ir druska"]),
                ("(?i)saldiklių", into["Cukrus, saldikliai ir druska"]),
                ("(?i)druska", into["Cukrus, saldikliai ir druska"]),
            });

            this.addMapper("Kruopos", new()
            {
                ("(?i)grikiai", into["Grikiai"]),
                ("(?i)grikių", into["Grikiai"]),
                ("(?i)perlinės", into["Perlinės kruopos"]),
                ("(?i)avinžirniai", into["Kitos kruopos"]),
                ("(?i)avinžiniai", into["Kitos kruopos"]),
                ("(?i)avižinės", into["Kitos kruopos"]),
                ("(?i)avižinė", into["Kitos kruopos"]),
                ("(?i)chia", into["Kitos kruopos"]),
                ("(?i)sorų", into["Kitos kruopos"]),
                ("(?i)perlinės", into["Kitos kruopos"]),
                ("(?i)linų", into["Kitos kruopos"]),
                ("(?i)kanapių", into["Kitos kruopos"]),
                ("(?i)kuskusas", into["Kitos kruopos"]),
                ("(?i)lęšiai", into["Kitos kruopos"]),
                ("(?i)baltos pupupelės", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)raudonosios pupupelės", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)žirniai", into["Kitos kruopos"]),
                ("(?i)kukurūzų", into["Kitos kruopos"]),
                ("(?i)manų", into["Manų kruopos"]),
                ("(?i)miežinės", into["Kvietinės ir miežinės kruopos"]),
                ("(?i)kvietinės", into["Kvietinės ir miežinės kruopos"]),
                ("(?i)basmati", into["Basmati ir kvapieji ryžiai"]),
                ("(?i)plikyti ryžiai", into["Plikyti ryžiai"]),
                ("(?i)ilgagrūdžiai", into["Ilgagrūdžiai ryžiai"]),
                ("(?i)rudi ilgagrūdžiai", into["Rudieji ir laukiniai ryžiai"]),
                ("(?i)juodieji", into["Rudieji ir laukiniai ryžiai"]),
                ("(?i)trumpagrūdžiai ryžiai", into["Trumpagrūdžiai ir apvaliagrūdžiai ryžiai"]),
                ("(?i)apvaliagrūdžiai ryžiai", into["Trumpagrūdžiai ir apvaliagrūdžiai ryžiai"]),
            });

            this.addMapper("Sėklos", new()
            {
                ("(?i)moliūgų sėklos", into["Sėklos"]),
                ("(?i)saulėgrąžų sėklos", into["Sėklos"]),
            });

            this.addMapper("Miltai", new()
            {
                ("(?i)kvietiniai miltai", into["Kvietiniai miltai"]),
                ("(?i)migdolų miltai", into["Kiti miltai"]),
                ("(?i)miltinis mišinys", into["Miltiniai mišiniai"]),
                ("(?i)grūdo", into["Pilno grūdo miltai"]),
                ("(?i)ruginiai miltai", into["Pilno grūdo miltai"]),
                ("(?i)kukurūzų miltai", into["Kiti miltai"]),
                ("(?i)ryžių miltai", into["Kiti miltai"]),
                ("(?i)avinžirnių miltai", into["Kiti miltai"]),
                ("(?i)žemės riešutų miltai", into["Kiti miltai"]),
                ("(?i)speltos miltai", into["Kiti miltai"]),
            });

            this.addMapper("Aliejus ir actas", new()
            {
                ("(?i)saulėgrąžų aliejus", into["Saulėgrąžų aliejus"]),
                ("(?i)rapsų aliejus", into["Rapsų aliejus"]),
                ("(?i)actas", into["Actas ir koncentruotos citrinų sultys"]),
                ("(?i)acto", into["Actas ir koncentruotos citrinų sultys"]),
                ("(?i)citrinų sultys", into["Actas ir koncentruotos citrinų sultys"]),
                ("(?i)alyvuogių aliejus", into["Alyvuogių aliejus"]),
                ("(?i)kokosų aliejus", into["Kokosų aliejus"]),
                ("(?i)linų sėmenų aliejus", into["Kitas aliejus"]),
                ("(?i)moliūgų sėklų aliejus", into["Kitas aliejus"]),
                ("(?i)vynuogių kauliukų aliejus", into["Kitas aliejus"]),
                ("(?i)graikinių riešutų aliejus", into["Kitas aliejus"]),
                ("(?i)sezamų aliejus", into["Kitas aliejus"]),
                ("(?i)migdolų aliejus", into["Kitas aliejus"]),
                ("(?i)kanapių aliejus", into["Kitas aliejus"]),
                ("(?i)ryžių aliejus", into["Kitas aliejus"]),
            });

            this.addMapper("Makaronai", new()
            {
                ("(?i)makaronai", into["Ilgieji ir plokštieji makaronai"]),
            });

            this.addMapper("Padažai", new()
            {
                ("(?i)pomidorų padažas", into["Pomidorų padažai ir pasta"]),
                ("(?i)adžika", into["Pomidorų padažai ir pasta"]),
                ("(?i)kečupas", into["Kečupai"]),
                ("(?i)ketchup", into["Kečupai"]),
                ("(?i)krienai", into["Krienai"]),
                ("(?i)garstyčios", into["Garstyčios"]),
                ("(?i)garstyčių", into["Garstyčios"]),
                ("(?i)majonezinis", into["Majoneziniai padažai"]),
                ("(?i)sadžiarūgštis", into["Majoneziniai padažai"]),
                ("(?i)pesto", into["Kiti padažai"]),
                ("(?i)cezario", into["Kiti padažai"]),
                ("(?i)česnakinis", into["Kiti padažai"]),
                ("(?i)bolonijos", into["Kiti padažai"]),
                ("(?i)vorčesterio", into["Kiti padažai"]),
                ("(?i)mėsainių", into["Kiti padažai"]),
                ("(?i)mėsainiams", into["Kiti padažai"]),
                ("(?i)barbekiu", into["Kiti padažai"]),
                ("(?i)salotų padažas", into["Kiti padažai"]),
                ("(?i)salsa", into["Kiti padažai"]),
                ("(?i)sojų", into["Sojų padažai"]),

            });


            this.addMapper("Tortai, pyragai", new()
            {
                ("(?i)vyniotinis", into["Plokštainiai ir vyniotiniai"]),
                ("(?i)pyragas", into["Plokštainiai ir vyniotiniai"]),
                ("(?i)tartaletė", into["Plokštainiai ir vyniotiniai"]),
                ("(?i)šakotis", into["Šakočiai ir skruzdėlynai"]),
                ("(?i)skurzdėlynas", into["Šakočiai ir skruzdėlynai"]),
                ("(?i)tortas", into["Tortai"]),
                ("(?i)torto", into["Tortai"]),
            });

            this.addMapper("Duona", new()
            {
                ("(?i)sumuštinių duona", into["Sumuštinių duona ir duonelės"]),
                ("(?i)skrudinimui", into["Sumuštinių duona ir duonelės"]),
                ("(?i)duonelė", into["Sumuštinių duona ir duonelės"]),
                ("(?i)duonelės", into["Sumuštinių duona ir duonelės"]),
                ("(?i)skrudinamoji duona", into["Sumuštinių duona ir duonelės"]),
                ("(?i)batonas", into["Batonas"]),
                ("(?i)tamsi", into["Tamsi duona"]),
                ("(?i)juoda", into["Tamsi duona"]),
                ("(?i)bočių", into["Tamsi duona"]),
                ("(?i)viso grūdo", into["Tamsi duona"]),
                ("(?i)ruginė", into["Tamsi duona"]),
                ("(?i)ajerų", into["Tamsi duona"]),
                ("(?i)šviesi", into["Šviesi duona"]),
                ("(?i)duona", into["Tamsi duona"]),
                ("(?i)kvietinė", into["Šviesi duona"]),
                ("(?i)batonas", into["Batonas"]),
            });

            this.addMapper("Bandelės", new()
            {
                ("(?i)bandelės", into["Saldžios bandelės"]),
                ("(?i)grietinėtis", into["Saldžios bandelės"]),
                ("(?i)kruasanas", into["Saldžios bandelės"]),
                ("(?i)keksiukai", into["Saldžios bandelės"]),
                ("(?i)spurgos", into["Saldžios bandelės"]),
                ("(?i)spurga", into["Saldžios bandelės"]),
                ("(?i)spurgytės", into["Saldžios bandelės"]),
                ("(?i)raguolis", into["Saldžios bandelės"]),
                ("(?i)ragelis", into["Saldžios bandelės"]),
                ("(?i)raguoliai", into["Saldžios bandelės"]),
                ("(?i)bandelė", into["Saldžios bandelės"]),
                ("(?i)riestainiai", into["Saldžios bandelės"]),
                
            });

            this.addMapper("Kiti duonos gaminiai", new()
            {
                ("(?i)fokačija", into["Kiti duonos gaminiai"]),
                ("(?i)picos padas", into["Trapučiai ir kiti paplotėliai"]),
                ("(?i)pynė", into["Kiti duonos gaminiai"]),
                ("(?i)traškutis", into["Kiti duonos gaminiai"]),
                ("(?i)traškučiai", into["Kiti duonos gaminiai"]),
                ("(?i)sumuštinis", into["Kiti duonos gaminiai"]),
                ("(?i)mėsainis", into["Kiti duonos gaminiai"]),
                ("(?i)džiūvėsiai", into["Kiti duonos gaminiai"]),
                ("(?i)kepta duona", into["Kiti duonos gaminiai"]),
                ("(?i)submarinas", into["Kiti duonos gaminiai"]),
                ("(?i)panini", into["Kiti duonos gaminiai"]),
                ("(?i)keksai", into["Kiti duonos gaminiai"]),
                ("(?i)duonos lazdelės", into["Kiti duonos gaminiai"]),
                ("(?i)kibinas", into["Kiti duonos gaminiai"]),
                ("(?i)trapučiai", into["Trapučiai ir kiti paplotėliai"]),
                ("(?i)konditeriniai krepšeliai", into["Trapučiai ir kiti paplotėliai"]),
                ("(?i)trapukai", into["Trapučiai ir kiti paplotėliai"]),
                ("(?i)paplotėliai", into["Trapučiai ir kiti paplotėliai"]),
                ("(?i)lavašas", into["Trapučiai ir kiti paplotėliai"]),
            });

            this.addMapper("Konditerijos gaminiai", new()
            {
                ("(?i)kanelės", into["Pyragaičiai ir desertai"]),
                ("(?i)ekleras", into["Pyragaičiai ir desertai"]),
                ("(?i)pyragėlis", into["Pyragaičiai ir desertai"]),
                ("(?i)pyragaitis", into["Pyragaičiai ir desertai"]),
                ("(?i)desertas", into["Pyragaičiai ir desertai"]),
                ("(?i)kūčiukai", into["Pyragaičiai ir desertai"]),
                ("(?i)riestainėliai", into["Pyragaičiai ir desertai"]),
                ("(?i)rageliai", into["Pyragaičiai ir desertai"]),
                ("(?i)žagarėliai", into["Pyragaičiai ir desertai"]),
                ("(?i)biskvitas", into["Pyragaičiai ir desertai"]),
                ("(?i)pynė", into["Pyragaičiai ir desertai"]),
                ("(?i)pynutė", into["Pyragaičiai ir desertai"]),
                ("(?i)sausainiai", into["Pyragaičiai ir desertai"]),
                ("(?i)sausučiai", into["Pyragaičiai ir desertai"]),
                ("(?i)javainiai", into["Pyragaičiai ir desertai"]),
                ("(?i)javinukai", into["Pyragaičiai ir desertai"]),
                ("(?i)zefyrai", into["Pyragaičiai ir desertai"]),
                ("(?i)sausainis", into["Pyragaičiai ir desertai"]),
                ("(?i)vafliai", into["Pyragaičiai ir desertai"]),
                ("(?i)vafliniai", into["Pyragaičiai ir desertai"]),
                ("(?i)meduoliai", into["Meduoliai"]),
            });

            this.addMapper("Grybai", new()
            {
                ("(?i)pievagrybiai", into["Grybai"]),
                ("(?i)grybai", into["Grybai"]),
                ("(?i)kreivabudės", into["Grybai"]),
                ("(?i)baravykas", into["Grybai"]),
                ("(?i)voveraitės", into["Grybai"]),
            });

            this.addMapper("Daržovės", new()
            {
                ("(?i)morkos", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)morkytės", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)bulvės", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)bulvių", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)kopūstai", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)agurkai", into["Pomidorai ir agurkai"]),
                ("(?i)pomidorai", into["Pomidorai ir agurkai"]),
                ("(?i)pomidoriukai", into["Pomidorai ir agurkai"]),
                ("(?i)svogūnai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)svogūnų", into["Svogūnai, porai ir česnakai"]),
                ("(?i)laiškai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)porai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)česnakai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)petražolės", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)petražolių", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)mėtos", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)mėta", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)bazilikai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)šalavijai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)rozmarinai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)rozmarinas", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)kalendros", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)čiobreliai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)čiobrelis", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)krapai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)krapų", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)žalumynų", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)paprika", into["Paprikos ir baklažanai"]),
                ("(?i)paprikos", into["Paprikos ir baklažanai"]),
                ("(?i)baklažanai", into["Paprikos ir baklažanai"]),
                ("(?i)avokadai", into["Avokadai"]),
                ("(?i)salotų mišinys", into["Salotos ir jų mišiniai"]),
                ("(?i)daigai", into["Salotos ir jų mišiniai"]),
                ("(?i)salotos", into["Salotos ir jų mišiniai"]),
                ("(?i)špinatai", into["Salotos ir jų mišiniai"]),
                ("(?i)gražgarstė", into["Salotos ir jų mišiniai"]),
                ("(?i)būrokėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)imbierai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)imbieras", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)burokėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)salierai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikėlių", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikas", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)kaliaropės", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)kukurūzai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)kukurūzų", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)žirniai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)žirneliai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)pupupelės", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)smidrai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)smidrų", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)moliūgai", into["Moliūgai ir cukinijos"]),
                ("(?i)moliūgas", into["Moliūgai ir cukinijos"]),
                ("(?i)cukinija", into["Moliūgai ir cukinijos"]),
                ("(?i)cukinijos", into["Moliūgai ir cukinijos"]),
                ("(?i)brokoliai", into["Brokoliai"]),
                ("(?i)paprika", into["Paprikos ir baklažanai"]),
            });

            this.addMapper("Vaisiai ir uogos", new()
            {
                ("(?i)bananai", into["Bananai"]),
                ("(?i)mandarinai", into["Citrusiniai vaisiai"]),
                ("(?i)citrinos", into["Citrusiniai vaisiai"]),
                ("(?i)greipfrutai", into["Citrusiniai vaisiai"]),
                ("(?i)klementinai", into["Citrusiniai vaisiai"]),
                ("(?i)apelsinai", into["Citrusiniai vaisiai"]),
                ("(?i)kiviai", into["Egzotiniai vaisiai"]),
                ("(?i)pasiflora", into["Egzotiniai vaisiai"]),
                ("(?i)pasiflorai", into["Egzotiniai vaisiai"]),
                ("(?i)mangai", into["Egzotiniai vaisiai"]),
                ("(?i)mangų", into["Egzotiniai vaisiai"]),
                ("(?i)datulės", into["Egzotiniai vaisiai"]),
                ("(?i)papajos", into["Egzotiniai vaisiai"]),
                ("(?i)ananasas", into["Egzotiniai vaisiai"]),
                ("(?i)kokoso", into["Egzotiniai vaisiai"]),
                ("(?i)kokosas", into["Egzotiniai vaisiai"]),
                ("(?i)persimonai", into["Egzotiniai vaisiai"]),
                ("(?i)granatai", into["Egzotiniai vaisiai"]),
                ("(?i)kinkanai", into["Egzotiniai vaisiai"]),
                ("(?i)obuoliai", into["Obuoliai ir kriaušės"]),
                ("(?i)kertuotis", into["Egzotiniai vaisiai"]),
                ("(?i)obuolys", into["Obuoliai ir kriaušės"]),
                ("(?i)kriaušės", into["Obuoliai ir kriaušės"]),
                ("(?i)kriaušė", into["Obuoliai ir kriaušės"]),
                ("(?i)vynuogės", into["Vynuogės ir uogos"]),
                ("(?i)gervuogės", into["Vynuogės ir uogos"]),
                ("(?i)avietės", into["Vynuogės ir uogos"]),
                ("(?i)šilauogės", into["Vynuogės ir uogos"]),
                ("(?i)spanguolės", into["Vynuogės ir uogos"]),
                ("(?i)melionai", into["Melionai"]),
                ("(?i)melionas", into["Melionai"]),
                ("(?i)arbūzas", into["Melionai"]),
                ("(?i)arbūzai", into["Melionai"]),
                ("(?i)persikas", into["Kaulavaisiai"]),
                ("(?i)abrikosas", into["Kaulavaisiai"]),
                ("(?i)slyva", into["Kaulavaisiai"]),
                ("(?i)slyvos", into["Kaulavaisiai"]),
            });

            this.addMapper("Šviežia paukštiena", new()
            {
                ("(?i)vištiena", into["Vištiena"]),
                ("(?i)šviežias viščiukas", into["Vištiena"]),
                ("(?i)viščiukų", into["Vištiena"]),
                ("(?i)vištienos", into["Vištiena"]),
                ("(?i)broilerių", into["Vištiena"]),
                ("(?i)kalakutiena", into["Kalakutiena"]),
                ("(?i)kalakutų", into["Kalakutiena"]),
                ("(?i)kalakutienos", into["Kalakutiena"]),
                ("(?i)antiena", into["Antiena"]),
                ("(?i)antienos", into["Antienos"]),
            });


            this.addMapper("Virtos mėsos gaminiai", new()
            {
                ("(?i)virta dešra", into["Virtos dešros"]),
                ("(?i)virtos dešros", into["Virtos dešros"]),
            });

            this.addMapper("Karštai rūkyti mėsos gaminiai", new()
            {
                ("(?i)karštai rūkyta", into["Karštai rūkyti gaminiai"]),
                ("(?i)karštai rūkytas", into["Karštai rūkyti gaminiai"]),
            });

            this.addMapper("Vytintos mėsos produktai", new()
            {
                ("(?i)vytinta", into["Vytinti mėsos gaminiai"]),
                ("(?i)vytintų", into["Vytinti mėsos gaminiai"]),
                ("(?i)vytintas", into["Vytinti mėsos gaminiai"]),
            });

            this.addMapper("Šaltai rūkyti gaminiai", new()
            {
                ("(?i)šaltai rūkyta", into["Šaltai rūkyti gaminiai"]),
                ("(?i)šaltai rūkytas", into["Šaltai rūkyti gaminiai"]),
            });

            this.addMapper("Dešrelės", new()
            {
                ("(?i)virtos dešrelės", into["Virtos dešrelės"]),
                ("(?i)pieniškos dešrelės", into["Virtos dešrelės"]),
                ("(?i)kabanossi", into["Virtos dešrelės"]),
                ("(?i)kabanosy", into["Virtos dešrelės"]),
                ("(?i) sardelės", into["Virtos dešrelės"]),
                ("(?i)virtos pieniškos dešrelės", into["Virtos dešrelės"]),
                ("(?i)kepamos dešrelės", into["Dešrelės griliui"]),
                ("(?i)kepamosios vištienos dešrelės", into["Dešrelės griliui"]),
                ("(?i)kepamosios kiaulienos dešrelės", into["Dešrelės griliui"]),
            });

            this.addMapper("Mėsos gaminiai", new()
            {
                ("(?i)paštetas", into["Paštetai ir kiti gaminiai"]),
                ("(?i)drebučiai", into["Paštetai ir kiti gaminiai"]),
                ("(?i)konservai", into["Mėsos ir paukštienos konservai"]),
                ("(?i)kotletas", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)plovas", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)cepelinai", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)balandėliai", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)apkepas", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)bulvių plokštainis", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)netikras zuikis", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)balandėliai", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)kepsneliai", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)žemaičių blynai", into["Virti, kepti ir sūdyti gaminiai"]),
                ("(?i)šašlykas", into["Marinuota mėsa"]),
                ("(?i)marinuota", into["Marinuota mėsa"]),
                ("(?i)marinuotas", into["Marinuota mėsa"]),
                ("(?i)marinuoti", into["Marinuota mėsa"]),
                ("(?i)marinate", into["Marinuota mėsa"]),
            });

            this.addMapper("Šviežia mėsa", new()
            {
                ("(?i)kiauliena", into["Kiauliena"]),
                ("(?i)kiaulių", into["Kiauliena"]),
                ("(?i)kiaulienos", into["Kiauliena"]),
                ("(?i)jautiena", into["Jautiena"]),
                ("(?i)jaučių", into["Jautiena"]),
                ("(?i)jautienos", into["Jautiena"]),
                ("(?i)veršiena", into["Jautiena"]),
                ("(?i)veršienos", into["Jautiena"]),
                ("(?i)triušiena", into["Triušiena"]),
                ("(?i)triušio", into["Triušiena"]),
                ("(?i)triušienos", into["Triušienos"]),
                ("(?i)avienos", into["Aviena"]),
                ("(?i)aviena", into["Aviena"]),
                ("(?i)avies", into["Aviena"]),
            });

            this.addMapper("Pienas", new() {
				("(?i)gėrimas", into["Pieno gėrimai"]),
				("(?i)sojų", into["Sojų pienas"]),
				("(?i)ryžių", into["Augalinis pienas"]),
				("(?i)sutirštintas", into["Sutirštintas pienas"]),
				("(?i)avižų", into["Augalinis pienas"]),
                ("(?i)kokosų", into["Augalinis pienas"]),
                ("(?i)migdolų", into["Augalinis pienas"]),
				("(?i)pienas", into["Pasterizuotas pienas"]),
				("(?i)natūralus", into["Pasterizuotas pienas"])

            });
			this.addMapper("Kiaušiniai", new()
			{
				("(?i)vištų kiaušiniai", into["Vištų kiaušiniai"]),
                ("(?i)putpelių kiaušiniai", into["Putpelių kiaušiniai"]),
                ("(?i)kiaušiniai", into["Vištų kiaušiniai"])
            });
			this.addMapper("Jogurtas", new()
			{
				("(?i)persikais", into["Jogurtai su pagardais"]),
				("(?i)braškių",  into["Jogurtai su pagardais"]),
                ("(?i)vyšnių",  into["Jogurtai su pagardais"]),
                ("(?i)abrikosų",  into["Jogurtai su pagardais"]),
                ("(?i)braškėmis",  into["Jogurtai su pagardais"]),
                ("(?i)spanguolėmis",  into["Jogurtai su pagardais"]),
                ("(?i)mangais",  into["Jogurtai su pagardais"]),
                ("(?i)persikų",  into["Jogurtai su pagardais"]),
                ("(?i)bananų",  into["Jogurtai su pagardais"]),
                ("(?i)kriaušių",  into["Jogurtai su pagardais"]),
                ("(?i)vyšniomis",  into["Jogurtai su pagardais"]),
                ("(?i)figomis",  into["Jogurtai su pagardais"]),
                ("(?i)agrastais",  into["Jogurtai su pagardais"]),
                ("(?i)šilauogėmis",  into["Jogurtai su pagardais"]),
                ("(?i)bananais",  into["Jogurtai su pagardais"]),
                ("(?i)kiviais",  into["Jogurtai su pagardais"]),
                ("(?i)vanile",  into["Jogurtai su pagardais"]),
                ("(?i)ananasais",  into["Jogurtai su pagardais"]),
                ("(?i)mėlynėmis",  into["Jogurtai su pagardais"]),
                ("(?i)obuoliais",  into["Jogurtai su pagardais"]),
                ("(?i)slyvomis",  into["Jogurtai su pagardais"]),
                ("(?i)apelsinų",  into["Jogurtai su pagardais"]),
                ("(?i)miško uogomis",  into["Jogurtai su pagardais"]),
                ("(?i)be laktozės", into["Jogurtai ir desertai be laktozės"]),
                ("(?i)natūralus", into["Jogurtai be pagardų"])
            });
            this.addMapper("Grietinė", new()
            {
                ("(?i).*", into["Grietinė"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i)jogurtinis gėrimas", into["Geriamieji jogurtai"]),
                ("(?i)geriamasis", into["Geriamieji jogurtai"]),
                ("(?i)geriamas", into["Geriamieji jogurtai"]),
                ("(?i)be laktozės", into["Jogurtai ir desertai be laktozės"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i).*", into["Geriamieji jogurtai"])
            });
            this.addMapper("Varškės Sūreliai", new()
            {
                ("(?i).*", into["Varškės sūreliai"])
            });
            this.addMapper("Kefyras, pasukos, rūgpienis", new()
            {
                ("(?i)kefyras", into["Kefyras ir kefyro gėrimai"]),
                ("(?i)kefyro", into["Kefyras ir kefyro gėrimai"]),
                ("(?i)rūgpienis", into["Rūgpienis"]),
                ("(?i)rauginto", into["Raugintos pasukos"])

            });
            this.addMapper("Sviestas, margarinas, riebalai", new()
            {
                ("(?i)sviestas", into["Sviestas"]),
                ("(?i)margarinas", into["Margarinas"]),
                ("(?i)tepinys", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepieji", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepiųjų", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepus", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepusis", into["Tepieji riebalų mišiniai"])
            });
            this.addMapper("Minkšti sūriai", new()
            {
                ("(?i)dešrelės", into["Sūrio užkandžiai ir sūrio lazdelės"]),
                ("(?i)užkandis", into["Sūrio užkandžiai ir sūrio lazdelės"]),
                ("(?i)lydytas", into["Lydyti sūriai"]),
                ("(?i)tepamas", into["Tepamieji sūriai"]),
                ("(?i)tepamasis", into["Tepamieji sūriai"]),
                ("(?i)pelėsinis", into["Pelėsiniai sūriai"]),
                ("(?i)fetos", into["Fetos ir brinzos sūriai"]),
                ("(?i)maskarponės", into["Maskarponės ir rikotos sūriai"]),
                ("(?i)buratta", into["Mocarelos ir buratos sūriai"]),
                ("(?i)mozzarella", into["Mocarelos ir buratos sūriai"]),
                ("(?i)sūris", into["Fermentiniai sūriai"]),

            });
            this.addMapper("Puskiečiai ir kieti sūriai", new()
            {
                ("(?i).*", into["Kietieji sūriai"])
            });
            this.addMapper("Varškės sūriai", new()
            {
                ("(?i).*", into["Varškės sūriai"])
            });
            this.addMapper("Varškė", new()
            {
                ("(?i).*", into["Varškė"])
            });
            this.addMapper("Majonezas", new()
            {
                ("(?i).*", into["Majonezas"])
            });
            this.addMapper("Varškė Desertai, Užtepėlės", new()
            {
                ("(?i)grūdėta", into["Grūdėta varškė"]),
                ("(?i)sūrelis", into["Varškės sūreliai"]),
                ("(?i)varškytė", into["Desertinė varškė"]),
                ("(?i)tepamoji", into["Tepamoji varškė"]),
                ("(?i)užtepėlė", into["Tepamoji varškė"]),
                ("(?i)figomis", into["Desertinė varškė"]),
                ("(?i)mangais", into["Desertinė varškė"]),
                ("(?i)šilauogėmis", into["Desertinė varškė"]),
                ("(?i)slyvomis", into["Desertinė varškė"]),
                ("(?i)braškėmis", into["Desertinė varškė"]),
            });
            this.addMapper("Produktai be laktozės", new()
            {
                ("(?i)pienas", into["Pienas ir gėrimai be laktozės"])
               
            });
            this.addForUnmapped(into["UNMAPPED"]);

            var items = root_cat
                .GetWithoutChildren()
                .ToList();

            this.executeMapper(items);
		}
	}
}

