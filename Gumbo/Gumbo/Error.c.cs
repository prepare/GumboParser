//Apache2, 2017, WinterDev
//
namespace Gumbo
{
    partial class GumboParser
    {
        GumboError gumbo_add_error()
        {
            //line: 157
            throw new TODOImplementException();
        }
        //GumboError* gumbo_add_error(GumboParser* parser)
        //{
        //    int max_errors = parser->_options->max_errors;
        //    if (max_errors >= 0 && parser->_output->errors.length >= (unsigned int) max_errors) {
        //        return NULL;
        //    }
        //    GumboError* error = gumbo_parser_allocate(parser, sizeof(GumboError));
        //    gumbo_vector_add(parser, error, &parser->_output->errors);
        //    return error;
        //}
    }
}
