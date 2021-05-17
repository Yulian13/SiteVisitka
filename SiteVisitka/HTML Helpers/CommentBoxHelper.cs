using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SiteVisitka.HTML_Helpers
{
    public static class CommentBoxHelper
    {
        /*
         <div class="row comment">
			<div class="container">
				<div class="row">
					<div class="col-md-6 Name">@contextName</div>
					<div class="col-md-6 Adress">@contextAddress</div>
				</div>
				<div class="row">
					<div class="col-md-12 TextComment">
						<p>@contextText</p>
					</div>
				</div>
			</div>
		</div>
        */

        public static HtmlString CreateCommentBox(this IHtmlHelper html, IMyTagCreator tagName, IMyTagCreator tagAddress, IMyTagCreator tagText, IMyTagCreator tagButton = null)
        {
            var writer = new System.IO.StringWriter();

            TagBuilder divName = CreatTagDiv("col-md-6 Name", tagName);
            TagBuilder divAddress = CreatTagDiv("col-md-6 Adress", tagAddress);
            TagBuilder divText = CreatTagDiv("col-md-12 TextComment", tagText);
            divText.InnerHtml.AppendHtml(tagButton?.CreateTag());

            TagBuilder divRowField = CreatTagDiv("row RowCommentField", divName, divAddress);
            TagBuilder divRowText = CreatTagDiv("row", divText);

            TagBuilder divContainer = CreatTagDiv("container", divRowField, divRowText);

            TagBuilder divMainRow = CreatTagDiv("row comment", divContainer);

            divMainRow.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString().Replace("&lt;", "<").Replace("&gt;", ">"));
        }

        private static TagBuilder CreatTagDiv(string CssClases, IMyTagCreator CreatorTag)
        {
            TagBuilder CreatedDiv = new("div");
            CreatedDiv.AddCssClass(CssClases);
            CreatedDiv.InnerHtml.AppendHtml(CreatorTag?.CreateTag());

            return CreatedDiv;
        }

        private static TagBuilder CreatTagDiv(string CssClases, params TagBuilder[] tagContexts)
        {
            TagBuilder CreatedTag = new("div");
            CreatedTag.AddCssClass(CssClases);

            foreach (TagBuilder tag in tagContexts)
            {
                CreatedTag.InnerHtml.AppendHtml(tag);
            }

            return CreatedTag;
        }

    }
    public interface IMyTagCreator
    {
        TagBuilder CreateTag();
    }

    public class MyCreatorTagP : IMyTagCreator
    {
        private readonly string _context;

        public MyCreatorTagP(string context)
        {
            _context = context;
        }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("p");
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

    public class MyCreatorTagInput : IMyTagCreator
    {
        private readonly string _name;
        private readonly string _placeholder;
        private readonly string _cssClass;
        private readonly string _type;

        public MyCreatorTagInput( string type, string cssClass, string name, string placeholder = "")
        {
            _name = name;
            _placeholder = placeholder;
            _cssClass = cssClass;
            _type = type;
        }


        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("input");
            tag.MergeAttribute("type",_type);
            tag.MergeAttribute("name", _name);
            tag.MergeAttribute("placeholder", _placeholder);
            tag.AddCssClass(_cssClass);
            return tag;
        }
    }

    public class MyCreatorTagTextarea : IMyTagCreator
    {
        private readonly string _name;
        private readonly string _context;
        private readonly string _cssClass;
        private readonly string _placeholder;

        private const string _style = "height: 110px;";

        public MyCreatorTagTextarea(string name, string context, string cssClass, string placeholder)
        {
            _name = name;
            _context = context;
            _cssClass = cssClass;
            _placeholder = placeholder;
        }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("textarea");
            tag.AddCssClass(_cssClass);
            tag.MergeAttribute("name", _name);
            tag.MergeAttribute("style", _style);
            tag.MergeAttribute("placeholder", _placeholder);
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

    public class MyCreatorTagButton : IMyTagCreator
    {
        private readonly string _type;
        private readonly string _context;
        private readonly string _cssClass;
        private readonly string _id;
        private readonly string _onClick;

        public MyCreatorTagButton(string type, string context, string id="", string onClick="", string cssClass="")
        {
            _type = type;
            _context = context;
            _cssClass = cssClass;
            _id = id;
            _onClick = onClick;
        }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("button");
            tag.AddCssClass(_cssClass);
            tag.MergeAttribute("type", _type);
            tag.MergeAttribute("id", _id);
            tag.MergeAttribute("onClick", _onClick);
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

}
