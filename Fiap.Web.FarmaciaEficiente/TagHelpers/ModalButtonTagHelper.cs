using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;


namespace Fiap.Web.FarmaciaEficiente.TagHelpers;

public class ModalLinkTagHelper : TagHelper
{
    public string Message { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.Attributes.SetAttribute("class", "btn btn-primary px-4 py-2 mb-4 float-end");
        output.Attributes.SetAttribute("href", "#");
        output.Attributes.SetAttribute("data-toggle", "modal");
        output.Attributes.SetAttribute("data-target", "#modalSubscriptionForm");

        var icon = "<svg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='currentColor' class='bi bi-plus' viewBox='0 0 16 16'><path d='M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z'></path></svg>";
        output.Content.SetHtmlContent($"{icon} Cadastrar");
        await base.ProcessAsync(context, output);
    }
}
