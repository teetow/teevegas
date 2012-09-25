using System;
using System.Runtime.InteropServices;

namespace NAudio.FileFormats.Ogg
{
	/// <summary>
	/// Summary description for OggInterop.
	/// </summary>
	internal class OggInterop
	{
		//extern int vorbis_encode_setup_init(vorbis_info *vi);
		[DllImport("vorbisenc.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_encode_setup_init(IntPtr vi);

		//int vorbis_encode_init_vbr(vorbis_info *vi,long channels,long rate,float base_quality);
		[DllImport("vorbisenc.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_encode_init_vbr(IntPtr vorbis_info, int channels, int rate, float base_quality);

		//extern void vorbis_info_init(vorbis_info *vi);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_info_init(IntPtr vorbis_info);

		//extern void vorbis_comment_init(vorbis_comment *vc);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_comment_init(IntPtr vorbis_comment);

		//extern void vorbis_comment_add_tag(vorbis_comment *vc, char *tag, char *contents);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_comment_add_tag(IntPtr vorbis_comment, string comment_tag, string comment_contents);

		//extern int vorbis_analysis_init(vorbis_dsp_state *v,vorbis_info *vi);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_analysis_init(IntPtr vd, IntPtr vi);

		//extern int vorbis_block_init(vorbis_dsp_state *v, vorbis_block *vb);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_block_init(IntPtr vd, IntPtr vb);

		//extern int vorbis_analysis_headerout(vorbis_dsp_state *v,vorbis_comment *vc,ogg_packet *op,ogg_packet *op_comm,ogg_packet *op_code);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_analysis_headerout(IntPtr vd, IntPtr vc, IntPtr header, IntPtr header_comm,
		                                                     IntPtr header_code);

		//extern float **vorbis_analysis_buffer(vorbis_dsp_state *v,int vals);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern IntPtr vorbis_analysis_buffer(IntPtr vd, int vals);

		//extern int vorbis_analysis_wrote(vorbis_dsp_state *v,int vals);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_analysis_wrote(IntPtr vd, int vals);

		//extern int vorbis_analysis_blockout(vorbis_dsp_state *v,vorbis_block *vb);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_analysis_blockout(IntPtr vd, IntPtr vb);

		//extern int vorbis_analysis(vorbis_block *vb,ogg_packet *op);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_analysis(IntPtr vb, IntPtr op);

		//extern int vorbis_bitrate_addblock(vorbis_block *vb);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_bitrate_addblock(IntPtr vb);

		//extern int vorbis_bitrate_flushpacket(vorbis_dsp_state *vd,ogg_packet *op);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_bitrate_flushpacket(IntPtr vd, IntPtr op);

		//extern int vorbis_block_clear(vorbis_block *vb);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern int vorbis_block_clear(IntPtr vb);

		//extern void vorbis_dsp_clear(vorbis_dsp_state *v);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_dsp_clear(IntPtr vd);

		//extern void vorbis_comment_clear(vorbis_comment *vc);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_comment_clear(IntPtr vc);

		//extern void vorbis_info_clear(vorbis_info *vi);
		[DllImport("vorbis.dll", CharSet = CharSet.Ansi)]
		internal static extern void vorbis_info_clear(IntPtr vi);

		//extern int ogg_stream_pageout(ogg_stream_state *os, ogg_page *og);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_stream_pageout(IntPtr os, IntPtr /*ref ogg_page*/ og);

		//extern int ogg_page_eos(ogg_page *og);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_page_eos( /*ref ogg_page*/ IntPtr og);

		//extern int ogg_stream_init(ogg_stream_state *os,int serialno);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_stream_init(IntPtr os, int serial_number);

		//extern int ogg_stream_clear(ogg_stream_state *os);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_stream_clear(IntPtr os);

		//extern int ogg_stream_packetin(ogg_stream_state *os, ogg_packet *op);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_stream_packetin(IntPtr os, IntPtr header);

		//extern int ogg_stream_flush(ogg_stream_state *os, ogg_page *og);
		[DllImport("ogg.dll", CharSet = CharSet.Ansi)]
		internal static extern int ogg_stream_flush(IntPtr os, /*ref ogg_page*/ IntPtr og);
	}
}