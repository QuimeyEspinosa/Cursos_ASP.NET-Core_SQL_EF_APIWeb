#pragma checksum "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e32ed35bd7d0a85674f68ea294f350a44667966"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Paginator), @"mvc.1.0.view", @"/Views/Shared/_Paginator.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e32ed35bd7d0a85674f68ea294f350a44667966", @"/Views/Shared/_Paginator.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3020564cfdc9765f49d55435a4ba3dac0d5190e0", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Paginator : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Concesionario.Models.Paginator>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
  
    var pagesCount = (int)Math.Ceiling((double)Model.TotalItems / Model.SizePage);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!--Funcionalidad: Anterior y siguiente-->\r\n\r\n");
            WriteLiteral("\r\n<!--Funcionalidad: Páginas-->\r\n\r\n");
#nullable restore
#line 35 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
  
    int initial = 1;
    var radio = 3;
    var maxPagesCount = radio * 2 + 1;
    int final = (pagesCount > maxPagesCount) ? maxPagesCount : pagesCount;
    if (Model.CurrentPage > radio + 1)
    {
        initial = Model.CurrentPage - radio;
        if (pagesCount > Model.CurrentPage + radio)
        {
            final = Model.CurrentPage + radio;
        }
        else
        {
            final = pagesCount;
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"mpagination\">\r\n    <ul>\r\n        <li>");
#nullable restore
#line 56 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
       Write(Html.ActionLink("Primera", null, new { page = 1 }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 57 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
         for (int i = initial; i <= final; i++)
        {
            if (i == Model.CurrentPage)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"active\">");
#nullable restore
#line 61 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
                              Write(Html.ActionLink(i.ToString(), null, new { page = i }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 62 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>");
#nullable restore
#line 65 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
               Write(Html.ActionLink(i.ToString(), null, new { page = i }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 66 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>");
#nullable restore
#line 68 "C:\Users\Quimey\source\repos\Concesionario\Concesionario\Views\Shared\_Paginator.cshtml"
       Write(Html.ActionLink("Ultima", null, new { page = pagesCount }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n    </ul>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Concesionario.Models.Paginator> Html { get; private set; }
    }
}
#pragma warning restore 1591
