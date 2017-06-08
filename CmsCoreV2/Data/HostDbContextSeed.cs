using CmsCoreV2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Data
{
    public static class HostDbContextSeed
    {
        public static void Seed(this HostDbContext context)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            // Look for any tenants record.
            if (context.AppTenants.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            
            var theme = AddTheme(context);
            AddAppTenants(context, theme);

        }

        public static void AddAppTenants(HostDbContext context, Theme theme)
        {
            var appTenant = new AppTenant();
            appTenant.Name = "BilgiKoleji";
            appTenant.Title = "Bilgi Koleji";
            appTenant.Hostname = "localhost:60002";
            appTenant.ThemeName = theme.Name;
            appTenant.ConnectionString = $"Server=.;Database={appTenant.Name};Trusted_Connection=True;MultipleActiveResultSets=true";
            appTenant.Folder = "bilgikoleji";
            appTenant.Theme = theme;
            appTenant.ThemeId = theme.Id;
            context.AppTenants.Add(appTenant);

            var appTenant2 = new AppTenant();
            appTenant2.Name = "BirInsan";
            appTenant2.Title = "Bir İnsan";
            appTenant2.Hostname = "localhost:60001";
            appTenant2.ThemeName = theme.Name;
            appTenant2.ConnectionString = $"Server=.;Database={appTenant2.Name};Trusted_Connection=True;MultipleActiveResultSets=true";
            appTenant2.Folder = "birinsan";
            appTenant2.Theme = theme;
            appTenant2.ThemeId = theme.Id;
            context.AppTenants.Add(appTenant2);
            context.SaveChanges();
        }
        public static Theme AddTheme(HostDbContext context)
        {
            var defaultTheme = new Theme();
            defaultTheme.Name = "edugate";
            defaultTheme.Logo = "";
            defaultTheme.ImageUrl = "";
            defaultTheme.MetaDescription = "";
            defaultTheme.MetaTitle = "";
            defaultTheme.MetaKeywords = "";
            defaultTheme.CreateDate = DateTime.Now;
            defaultTheme.UpdateDate = DateTime.Now;
            defaultTheme.CreatedBy = "UserName";
            defaultTheme.UpdatedBy = "UserName";
            defaultTheme.CustomCSS = ".dots-loader:not(:required) {\n" +
"    box-shadow: rgb(233,29,177) -14px -14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    -webkit-animation: dots-loader2 5s infinite ease-in-out;\n" +
"    animation: dots-loader2 5s infinite ease-in-out;\n" +
"}\n" +
"@keyframes dots-loader2 {\n" +
"    0% {\n" +
"        box-shadow: rgb(233,29,177) -14px -14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"    8.33% {\n" +
"        box-shadow: rgb(233,29,177) 14px -14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"    16.67% {\n" +
"        box-shadow: rgb(233,29,177) 14px 14px 0 7px, rgb(248,156,36) 14px 14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"    25% {\n" +
"        box-shadow: rgb(233,29,177) -14px 14px 0 7px, rgb(248,156,36) -14px 14px 0 7px, rgb(63,174,72) -14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"    33.33% {\n" +
"        box-shadow: rgb(233,29,177) -14px -14px 0 7px, rgb(248,156,36) -14px 14px 0 7px, rgb(63,174,72) -14px -14px 0 7px, rgb(0,130,190) -14px -14px 0 7px;\n" +
"    }\n" +
"    41.67% {\n" +
"        box-shadow: rgb(233,29,177) 14px -14px 0 7px, rgb(248,156,36) -14px 14px 0 7px, rgb(63,174,72) -14px -14px 0 7px, rgb(0,130,190) 14px -14px 0 7px;\n" +
"    }\n" +
"    50% {\n" +
"        box-shadow: rgb(233,29,177) 14px 14px 0 7px, rgb(248,156,36) -14px 14px 0 7px, rgb(63,174,72) -14px -14px 0 7px, rgb(0,130,190) 14px -14px 0 7px;\n" +
"    }\n" +
"    58.33% {\n" +
"        box-shadow: rgb(233,29,177) -14px 14px 0 7px, rgb(248,156,36) -14px 14px 0 7px, rgb(63,174,72) -14px -14px 0 7px, rgb(0,130,190) 14px -14px 0 7px;\n" +
"    }\n" +
"    66.67% {\n" +
"        box-shadow: rgb(233,29,177) -14px -14px 0 7px, rgb(248,156,36) -14px -14px 0 7px, rgb(63,174,72) -14px -14px 0 7px, rgb(0,130,190) 14px -14px 0 7px;\n" +
"    }\n" +
"    75% {\n" +
"        box-shadow: rgb(233,29,177) 14px -14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px -14px 0 7px, rgb(0,130,190) 14px -14px 0 7px;\n" +
"    }\n" +
"    83.33% {\n" +
"        box-shadow: rgb(233,29,177) 14px 14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) 14px 14px 0 7px;\n" +
"    }\n" +
"    91.67% {\n" +
"        box-shadow: rgb(233,29,177) -14px 14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"    100% {\n" +
"        box-shadow: rgb(233,29,177) -14px -14px 0 7px, rgb(248,156,36) 14px -14px 0 7px, rgb(63,174,72) 14px 14px 0 7px, rgb(0,130,190) -14px 14px 0 7px;\n" +
"    }\n" +
"}\n" +
"header .header-main .logo .header-logo img {\n" +
"    min-width: 176px;\n" +
"}\n" +
".page-link {\n" +
"    background-color: #242c42 !important;\n" +
"    color: white !important;\n" +
"    width: 40px;\n" +
"    height: 40px;\n" +
"    margin-right: 5px;\n" +
"    padding: 0px;\n" +
"}\n" +
".pagination > li:last-child > a {\n" +
"    border-radius: 0px;\n" +
"}\n" +
".pagination > li:first-child > a {\n" +
"    border-radius: 0px;\n" +
"}\n" +
".page-link last-child {\n" +
"    margin-right: 0px;\n" +
"}\n" +
".page-link:hover {\n" +
"    transition: background-color 0.3s linear;\n" +
"    background-color: #2aacff !important;\n" +
"    width: 40px;\n" +
"    height: 40px;\n" +
"}\n" +
".edugate-layout-2:before {\n" +
"    border-left: 3px solid #eaedf5;\n" +
"}\n" +
".edugate-layout-2:after {\n" +
"    width: 3px;\n" +
"}\n" +
".top-courses-slider:nth-child(1) .owl-item:nth-child(3) .edugate-layout-2:after {\n" +
"    background-color: rgb(88,169,207);\n" +
"}\n" +
".top-courses-slider:nth-child(1) .owl-item:nth-child(4) .edugate-layout-2:after {\n" +
"    background-color: rgb(233,29,117);\n" +
"}\n" +
".top-courses-slider:nth-child(1) .owl-item:nth-child(5) .edugate-layout-2:after {\n" +
"    background-color: rgb(88,169,207);\n" +
"}\n" +
".top-courses-slider:nth-child(1) .owl-item:nth-child(6) .edugate-layout-2:after {\n" +
"    background-color: rgb(233,29,117);\n" +
"}\n" +
".top-courses-slider:nth-child(2) .owl-item:nth-child(3) .edugate-layout-2:after {\n" +
"    background-color: rgb(248,156,36);\n" +
"}\n" +
".top-courses-slider:nth-child(2) .owl-item:nth-child(4) .edugate-layout-2:after {\n" +
"    background-color: rgb(63,174,72);\n" +
"}\n" +
".top-courses-slider:nth-child(2) .owl-item:nth-child(5) .edugate-layout-2:after {\n" +
"    background-color: rgb(248,156,36);\n" +
"}\n" +
".top-courses-slider:nth-child(2) .owl-item:nth-child(6) .edugate-layout-2:after {\n" +
"    background-color: rgb(63,174,72);\n" +
"}\n" +
".section-padding {\n" +
"    padding: 50px 0;\n" +
"}\n" +
"header .header-topbar {\n" +
"    background-color: #093085;\n" +
"}\n" +
"header .header-main .navigation .nav-links li .main-menu {\n" +
"    text-transform: capitalize;\n" +
"}\n" +
".fa-search {\n" +
"    color: rgb(233,29,117);\n" +
"}\n" +
"    .fa-search:hover {\n" +
"        color: #0082cb;\n" +
"    }\n" +
".slider-banner .owl-controls .owl-dots .owl-dot:nth-of-type(1) {\n" +
"    background-color: #eb1c74;\n" +
"    width: 20px;\n" +
"    height: 20px;\n" +
"    border: 0px;\n" +
"}\n" +
".slider-banner .owl-controls .owl-dots .owl-dot:nth-of-type(2) {\n" +
"    background-color: #ff9e19;\n" +
"    width: 20px;\n" +
"    height: 20px;\n" +
"    border: 0px;\n" +
"}\n" +
".slider-banner .owl-controls .owl-dots .owl-dot:nth-of-type(3) {\n" +
"    background-color: #3baf29;\n" +
"    width: 20px;\n" +
"    height: 20px;\n" +
"    border: 0px;\n" +
"}\n" +
".slider-banner .owl-controls .owl-dots .owl-dot:nth-of-type(4) {\n" +
"    background-color: #0082cb;\n" +
"    width: 20px;\n" +
"    height: 20px;\n" +
"    border: 0px;\n" +
"}\n" +
".slider-banner .owl-controls .owl-dots .owl-dot:nth-of-type(5) {\n" +
"    background-color: #092f87;\n" +
"    width: 20px;\n" +
"    height: 20px;\n" +
"    border: 0px;\n" +
"}\n" +
".slider-banner .owl-controls .owl-dots .owl-dot.active {\n" +
"    border: 2px solid white;\n" +
"}\n" +
"#floatingButton a {\n" +
"    position: fixed;\n" +
"    bottom: 20px;\n" +
"    height: 40px;\n" +
"    text-align: center;\n" +
"    font-size: 24px;\n" +
"    z-index: 100;\n" +
"    color: #2aacff;\n" +
"    transition: all .5s ease-in-out;\n" +
"    background-color: #0082cb;\n" +
"    width: 250px;\n" +
"    right: 65px;\n" +
"    color: white;\n" +
"}\n" +
"video {\n" +
"    position: absolute;\n" +
"    top: 50%;\n" +
"    left: 50%;\n" +
"    min-width: 100%;\n" +
"    min-height: 100%;\n" +
"    width: auto;\n" +
"    height: auto;\n" +
"    z-index: -100;\n" +
"    transform: translateX(-50%) translateY(-50%);\n" +
"    background: url('/edugate/images/video_foto.png') no-repeat;\n" +
"    background-size: cover;\n" +
"    transition: 1s opacity;\n" +
"}\n" +
".btn {\n" +
"    text-transform: capitalize;\n" +
"}\n" +
"#homeSlider2 .slider-item > div {\n" +
"    padding-top: 120px;\n" +
"}\n" +
".staff-item {\n" +
"    background-color: rgba(255,255,255,0.7);\n" +
"}\n" +
".positionRelative {\n" +
"    position: relative;\n" +
"}\n" +
".why-choose-us {\n" +
"    background-color: rgb(62,160,227);\n" +
"    background: url('/edugate/images/blue-background.png') no-repeat;\n" +
"    color: #fff;\n" +
"}\n" +
"li.accordion {\n" +
"    background-color: #eee;\n" +
"    color: #444;\n" +
"    cursor: pointer;\n" +
"    padding: 18px;\n" +
"    width: 100%;\n" +
"    text-align: left;\n" +
"    border: none;\n" +
"    outline: none;\n" +
"    transition: 0.4s;\n" +
"}\n" +
".choose-course .icon-circle {\n" +
"    margin: initial;\n" +
"}\n" +
".item-course.item-1 .name-course {\n" +
"    width: calc( 170px * 0.7 );\n" +
"    margin: 0 auto;\n" +
"}\n" +
".item-course.item-2 .name-course {\n" +
"    width: calc( 170px * 1 );\n" +
"    margin: 0 auto;\n" +
"}\n" +
".item-course.item-3 .name-course {\n" +
"    width: calc( 170px * 1.3 );\n" +
"    margin: 0 auto;\n" +
"}\n" +
".item-course.item-4 .name-course {\n" +
"    width: calc( 170px * 1.6 );\n" +
"    margin: 0 auto;\n" +
"}\n" +
".choose-course .item-4 .icon-circle {\n" +
"    margin: 0 auto;\n" +
"    margin-right: 10px;\n" +
"}\n" +
".choose-course .icon-circle .icon-background {\n" +
"    z-index: 99;\n" +
"}\n" +
".item-course.item-1 {\n" +
"    padding-top: 220px;\n" +
"}\n" +
"    .item-course.item-1 > .icon-circle {\n" +
"        \n" +
"        width: 119px;\n" +
"        height: 119px;\n" +
"        margin: 0px auto;\n" +
"    }\n" +
".choose-course .item-1 .icon-circle .icon-background {\n" +
"   background-color: rgba(233,29,177,0.7);\n" +
"   width:102.2px;\n" +
"   height:102.2px;\n" +
"   margin-bottom:-4.3px;\n" +
"   margin-left:-50.5px;\n" +
"}\n" +
".choose-course .item-1 .icon-circle .info .info-back:before {\n" +
"    border: 12px solid rgb(233,29,177);\n" +
"}\n" +
".choose-course .item-1 .name-course a {\n" +
"    color: rgb(233,29,177);\n" +
"}\n" +
".item-course.item-2 {\n" +
"    padding-top: 80px;\n" +
"    text-align: left;\n" +
"}\n" +
"    .item-course.item-2 > .icon-circle {\n" +
"        \n" +
"        margin: 0px auto;\n" +
"    }\n" +
".choose-course .item-2 .icon-circle .icon-background {\n" +
"    background-color: rgba(248,156,36,0.7);\n" +
"    \n" +
"}\n" +
".choose-course .item-2 .icon-circle .info .info-back:before {\n" +
"    border: 12px solid rgb(248,156,36);\n" +
"}\n" +
".choose-course .item-2 .name-course a {\n" +
"    color: rgb(248,156,36);\n" +
"}\n" +
".item-course.item-3 {\n" +
"    text-align: left;\n" +
"}\n" +
"    .item-course.item-3 > .icon-circle {\n" +
"        \n" +
"        width: 221px;\n" +
"        height: 221px;\n" +
"        margin: 0px auto;\n" +
"    }\n" +
".choose-course .item-3 .icon-circle .icon-background {\n" +
"    background-color: rgba(63,174,72,0.7);\n" +
"    width: 189.8px;\n" +
"    height: 189.8px;\n" +
"    margin-bottom:4.5px;\n" +
"    margin-left: -93px;\n" +
"}\n" +
".choose-course .item-3 .icon-circle .info .info-back:before {\n" +
"    border: 20px solid rgb(63,174,72);\n" +
"}\n" +
".choose-course .item-3 .name-course a {\n" +
"    color: rgb(63,174,72);\n" +
"}\n" +
".item-course.item-4 > .icon-circle {\n" +
"    \n" +
"    width: 272px;\n" +
"    height: 272px;\n" +
"    margin: 0px auto;\n" +
"}\n" +
".choose-course .item-4 .icon-circle .icon-background {\n" +
"    background-color: rgba(0,130,190,0.7);\n" +
"    width: 233.6px;\n" +
"    height: 233.6px;\n" +
"    margin-bottom:8px;\n" +
"    margin-left: -116.8px;\n" +
"}\n" +
".choose-course .item-4 .icon-circle .info .info-back:before {\n" +
"    border: 20px solid rgb(0,130,190);\n" +
"}\n" +
".choose-course .item-4 .name-course a {\n" +
"    color: rgb(0,130,190);\n" +
"}\n" +
".choose-course .icon-circle .info .info-back a {\n" +
"    padding: 0px;\n" +
"}\n" +
".choose-course .name-course a {\n" +
"    text-transform: capitalize\n" +
"}\n" +
".choose-course .icon-circle .info .info-back {\n" +
"    background-color: #FFFFFF;\n" +
"}\n" +
".choose-course {\n" +
"    background: url('/edugate/images/background-gradient-gray.png') repeat-y white;\n" +
"}\n" +
"#floatingButton #floatingForm {\n" +
"    position: fixed;\n" +
"    display: inline-block;\n" +
"    bottom: 65px;\n" +
"    height: 435px;\n" +
"    text-align: left;\n" +
"    z-index: 100;\n" +
"    color: gray;\n" +
"    font-size: 12px;\n" +
"    background-color: rgba(250,250,250,0.9);\n" +
"    border-top: 1px solid lightgray;\n" +
"    border-left: 1px solid lightgray;\n" +
"    border-bottom: 1px solid lightgray;\n" +
"    width: 251px;\n" +
"    right: 64px;\n" +
"    padding: 0px;\n" +
"    border-right: 2px solid #0082cb;\n" +
"}\n" +
"#floatingForm legend {\n" +
"    margin-bottom: 0px;\n" +
"}\n" +
"#floatingForm input.form-control, #floatingForm select.form-control {\n" +
"    font-size: 12px !important;\n" +
"}\n" +
"#floatingForm .btn-sm {\n" +
"    background: #5cb85c;\n" +
"}\n" +
"#floatingForm .formField {\n" +
"    margin: 9px 0px;\n" +
"}\n" +
"    #floatingForm .formField:first-child {\n" +
"        margin-top: 0px;\n" +
"    }\n" +
"/* Add a background color to the button if it is clicked on (add the .active class with JS), and when you move the mouse over it (hover) */\n" +
"li.accordion.active, li.accordion:hover {\n" +
"    background-color: #ddd;\n" +
"}\n" +
"div.CustomPanel {\n" +
"    padding: 0 18px;\n" +
"    background-color: white;\n" +
"    display: none;\n" +
"}\n" +
".dropdown-submenu {\n" +
"    position: relative;\n" +
"}\n" +
"    .dropdown-submenu > .dropdown-menu {\n" +
"        top: 0;\n" +
"        left: 100%;\n" +
"        margin-top: -6px;\n" +
"        margin-left: -1px;\n" +
"        -webkit-border-radius: 0 6px 6px 6px;\n" +
"        -moz-border-radius: 0 6px 6px;\n" +
"        border-radius: 0 6px 6px 6px;\n" +
"    }\n" +
"    .dropdown-submenu:hover > .dropdown-menu {\n" +
"        display: block;\n" +
"    }\n" +
"    .dropdown-submenu > a:after {\n" +
"        display: block;\n" +
"        content: \" \";\n" +
"        float: right;\n" +
"        width: 0;\n" +
"        height: 0;\n" +
"        border-color: transparent;\n" +
"        border-style: solid;\n" +
"        border-width: 5px 0 5px 5px;\n" +
"        border-left-color: #ccc;\n" +
"        margin-top: 5px;\n" +
"        margin-right: -10px;\n" +
"    }\n" +
"    .dropdown-submenu:hover > a:after {\n" +
"        border-left-color: #fff;\n" +
"    }\n" +
"    .dropdown-submenu.pull-left {\n" +
"        float: none;\n" +
"    }\n" +
"        .dropdown-submenu.pull-left > .dropdown-menu {\n" +
"            left: -100%;\n" +
"            margin-left: 10px;\n" +
"            -webkit-border-radius: 6px 0 6px 6px;\n" +
"            -moz-border-radius: 6px 0 6px 6px;\n" +
"            border-radius: 6px 0 6px 6px;\n" +
"        }\n" +
"header .header-main .edugate-dropdown-menu-1 li .link-page, header .header-main .edugate-dropdown-menu-2 li .link-page {\n" +
"    line-height: 30px;\n" +
"}\n" +
"@media screen and (max-width: 991px) {\n" +
"    .item-course{\n" +
"        padding:20px 0 !important;\n" +
"    }\n" +
"    .name-course{\n" +
"        margin-top:5px !important;\n" +
"    }\n" +
"}\n" +
".slider-item .slider-1, .slider-item .slider-2, .slider-item .slider-3 {\n" +
"    background-image: none !important;\n" +
"}\n";

            defaultTheme.MenuLocations = "Primary";
            defaultTheme.ComponentTemplates = "Default,Gallery,MiniGallery,ContactForm,JobApplicationForm,PreRegistrationForm,SurveyForm,LogoSlider,Secondary";
            defaultTheme.PageTemplates = "Page,Blog,Contact,Gallery,Index,JobApplication,Post,Posts,PreRegistration,Search,SiteMap,Survey";
            context.Themes.Add(defaultTheme);
            context.SaveChanges();
            return defaultTheme;
        }


    }
}
