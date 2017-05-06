
// Copyright 2010 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author: jdtang@google.com (Jonathan Tang)


namespace Gumbo
{
    using System.Text;
    using System.Collections.Generic;

    //// Because GumboStringPieces are immutable, we can't insert a character directly
    //// into a text node.  Instead, we accumulate all pending characters here and
    //// flush them out to a text node whenever a new element is inserted.
    ////
    //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#insert-a-character
    //typedef struct _TextNodeBufferState
    //{
    //    // The accumulated text to be inserted into the current text node.
    //    GumboStringBuffer _buffer;

    //    // A pointer to the original text represented by this text node.  Note that
    //    // because of foster parenting and other strange DOM manipulations, this may
    //    // include other non-text HTML tags in it; it is defined as the span of
    //    // original text from the first character in this text node to the last
    //    // character in this text node.
    //    const char* _start_original_text;

    //    // The source position of the start of this text node.
    //    GumboSourcePosition _start_position;

    //    // The type of node that will be inserted (TEXT, CDATA, or WHITESPACE).
    //    GumboNodeType _type;
    //}
    //TextNodeBufferState;

    class GumboStringBuffer { }

    struct TextBufferPointer
    {

    }

    class TextNodeBufferState
    {
        public GumboStringBuffer _buffer;
        public TextBufferPointer _start_original_text;
        public GumboSourcePosition _start_position;
        public GumboNodeType _type;
    }

    class GumboParserState
    {
        // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#insertion-mode
        public GumboInsertionMode _insertion_mode;

        //    // Used for run_generic_parsing_algorithm, which needs to switch back to the
        //    // original insertion mode at its conclusion.
        public GumboInsertionMode _original_insertion_mode;

        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-stack-of-open-elements
        //    GumboVector /*GumboNode*/ _open_elements;
        public List<GumboNode> _open_elements;

        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-list-of-active-formatting-elements
        //    GumboVector /*GumboNode*/ _active_formatting_elements;
        public List<GumboNode> _active_formatting_elements;

        //    // The stack of template insertion modes.
        //    // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#the-insertion-mode
        //    GumboVector /*InsertionMode*/ _template_insertion_modes;
        public List<GumboInsertionMode> _template_insertion_modes;


        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-element-pointers
        //    GumboNode* _head_element;
        //    GumboNode* _form_element;

        public GumboNode _head_element;
        public GumboNode _form_element;

        //    // The element used as fragment context when parsing in fragment mode
        //    GumboNode* _fragment_ctx;
        public GumboNode _fragment_ctx;

        //    // The flag for when the spec says "Reprocess the current token in..."
        //    bool _reprocess_current_token;
        public bool _reprocess_current_token;
        //    // The flag for "acknowledge the token's self-closing flag".
        //    bool _self_closing_flag_acknowledged;
        public bool _self_closing_flag_acknowledged;

        //    // The "frameset-ok" flag from the spec.
        //    bool _frameset_ok;
        public bool _frameset_ok;
        //    // The flag for "If the next token is a LINE FEED, ignore that token...".
        //    bool _ignore_next_linefeed;
        public bool _ignore_next_linefeed;

        //    // The flag for "whenever a node would be inserted into the current node, it
        //    // must instead be foster parented".  This is used for misnested table
        //    // content, which needs to be handled according to "in body" rules yet foster
        //    // parented outside of the table.
        //    // It would perhaps be more explicit to have this as a parameter to
        //    // handle_in_body and insert_element, but given how special-purpose this is
        //    // and the number of call-sites that would need to take the extra parameter,
        //    // it's easier just to have a state flag.
        //    bool _foster_parent_insertions;
        public bool _foster_parent_insertions;
        //    // The accumulated text node buffer state.
        //    TextNodeBufferState _text_node;
        public TextNodeBufferState _text_node;
        //    // The current token.
        //    GumboToken* _current_token;
        public GumboToken _current_token;
        //    // The way that the spec is written, the </body> and </html> tags are *always*
        //    // implicit, because encountering one of those tokens merely switches the
        //    // insertion mode out of "in body".  So we have individual state flags for
        //    // those end tags that are then inspected by pop_current_node when the <body>
        //    // and <html> nodes are popped to set the GUMBO_INSERTION_IMPLICIT_END_TAG
        //    // flag appropriately.
        //    bool _closed_body_tag;
        //    bool _closed_html_tag;
        public bool _closed_body_tag;
        public bool _closed_html_tag;


    }
    //typedef struct GumboInternalParserState
    //{
    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#insertion-mode
    //    GumboInsertionMode _insertion_mode;

    //    // Used for run_generic_parsing_algorithm, which needs to switch back to the
    //    // original insertion mode at its conclusion.
    //    GumboInsertionMode _original_insertion_mode;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-stack-of-open-elements
    //    GumboVector /*GumboNode*/ _open_elements;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-list-of-active-formatting-elements
    //    GumboVector /*GumboNode*/ _active_formatting_elements;

    //    // The stack of template insertion modes.
    //    // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#the-insertion-mode
    //    GumboVector /*InsertionMode*/ _template_insertion_modes;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-element-pointers
    //    GumboNode* _head_element;
    //    GumboNode* _form_element;

    //    // The element used as fragment context when parsing in fragment mode
    //    GumboNode* _fragment_ctx;

    //    // The flag for when the spec says "Reprocess the current token in..."
    //    bool _reprocess_current_token;

    //    // The flag for "acknowledge the token's self-closing flag".
    //    bool _self_closing_flag_acknowledged;

    //    // The "frameset-ok" flag from the spec.
    //    bool _frameset_ok;

    //    // The flag for "If the next token is a LINE FEED, ignore that token...".
    //    bool _ignore_next_linefeed;

    //    // The flag for "whenever a node would be inserted into the current node, it
    //    // must instead be foster parented".  This is used for misnested table
    //    // content, which needs to be handled according to "in body" rules yet foster
    //    // parented outside of the table.
    //    // It would perhaps be more explicit to have this as a parameter to
    //    // handle_in_body and insert_element, but given how special-purpose this is
    //    // and the number of call-sites that would need to take the extra parameter,
    //    // it's easier just to have a state flag.
    //    bool _foster_parent_insertions;

    //    // The accumulated text node buffer state.
    //    TextNodeBufferState _text_node;

    //    // The current token.
    //    GumboToken* _current_token;

    //    // The way that the spec is written, the </body> and </html> tags are *always*
    //    // implicit, because encountering one of those tokens merely switches the
    //    // insertion mode out of "in body".  So we have individual state flags for
    //    // those end tags that are then inspected by pop_current_node when the <body>
    //    // and <html> nodes are popped to set the GUMBO_INSERTION_IMPLICIT_END_TAG
    //    // flag appropriately.
    //    bool _closed_body_tag;
    //    bool _closed_html_tag;
    //}
    //GumboParserState;


    //----------------------------
    partial class GumboParser
    {
        GumboParserState parser_state;

        public GumboParser()
        {

        }
        void parser_state_init()
        {
            //TODO: review how to alloc new object


            //from line: 476
            //static void parser_state_init(GumboParser* parser)
            //{
            //    GumboParserState* parser_state =
            //        gumbo_parser_allocate(parser, sizeof(GumboParserState));
            //    parser_state->_insertion_mode = GUMBO_INSERTION_MODE_INITIAL;
            //    parser_state->_reprocess_current_token = false;
            //    parser_state->_frameset_ok = true;
            //    parser_state->_ignore_next_linefeed = false;
            //    parser_state->_foster_parent_insertions = false;
            //    parser_state->_text_node._type = GUMBO_NODE_WHITESPACE;
            //    gumbo_string_buffer_init(parser, &parser_state->_text_node._buffer);
            //    gumbo_vector_init(parser, 10, &parser_state->_open_elements);
            //    gumbo_vector_init(parser, 5, &parser_state->_active_formatting_elements);
            //    gumbo_vector_init(parser, 5, &parser_state->_template_insertion_modes);
            //    parser_state->_head_element = NULL;
            //    parser_state->_form_element = NULL;
            //    parser_state->_fragment_ctx = NULL;
            //    parser_state->_current_token = NULL;
            //    parser_state->_closed_body_tag = false;
            //    parser_state->_closed_html_tag = false;
            //    parser->_parser_state = parser_state;
            //}

            GumboParserState parser_state = new GumboParserState();
            parser_state._insertion_mode = GumboInsertionMode.GUMBO_INSERTION_MODE_INITIAL;
            parser_state._reprocess_current_token = false;
            parser_state._frameset_ok = true;
            parser_state._ignore_next_linefeed = false;
            parser_state._foster_parent_insertions = false;
            parser_state._text_node._type = GumboNodeType.GUMBO_NODE_WHITESPACE;
            parser_state._text_node._buffer = new GumboStringBuffer();
            parser_state._open_elements = new List<GumboNode>(10);
            parser_state._active_formatting_elements = new List<GumboNode>(5);
            parser_state._template_insertion_modes = new List<GumboInsertionMode>(5);
            //
            //... else fields are set to its default in C#
            this.parser_state = parser_state;
        }
        void parser_state_destroy()
        {
            //static void parser_state_destroy(GumboParser* parser)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    if (state->_fragment_ctx)
            //    {
            //        destroy_node(parser, state->_fragment_ctx);
            //    }
            //    gumbo_vector_destroy(parser, &state->_active_formatting_elements);
            //    gumbo_vector_destroy(parser, &state->_open_elements);
            //    gumbo_vector_destroy(parser, &state->_template_insertion_modes);
            //    gumbo_string_buffer_destroy(parser, &state->_text_node._buffer);
            //    gumbo_parser_deallocate(parser, state);
            //}
        }
        public GumboNode get_document_node()
        {
            return this._output.document;
        }
        //static GumboNode* get_document_node(GumboParser* parser)
        //{
        //    return parser->_output->document;
        //}
    }


}
