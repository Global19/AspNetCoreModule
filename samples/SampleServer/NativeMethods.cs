﻿using System;
using System.Runtime.InteropServices;

namespace SampleServer
{
    public static class NativeMethods
    {
        public const int S_OK = 0;

        public enum REQUEST_NOTIFICATION_STATUS
        {
            RQ_NOTIFICATION_CONTINUE,
            RQ_NOTIFICATION_PENDING,
            RQ_NOTIFICATION_FINISH_REQUEST
        }

        public delegate void request_handler_cb(int error, IntPtr pHttpContext, IntPtr state);
        public delegate void request_handler(IntPtr pHttpContext, request_handler_cb pfnCompletionCallback, IntPtr pvCompletionContext, IntPtr pvRequestContext);
        public delegate REQUEST_NOTIFICATION_STATUS PFN_ASYNC_COMPLETION(IntPtr pHttpContext, IntPtr completionInfo, IntPtr pvCompletionContext);

        [DllImport("aspnetcore.dll")]
        public static extern void register_request_callback(request_handler callback, IntPtr pvRequestContext);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern int http_write_response_bytes(IntPtr pHttpContext, byte* pvBuffer, int cbBuffer, PFN_ASYNC_COMPLETION pfnCompletionCallback, IntPtr pvCompletionContext, out bool fCompletionExpected);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern int http_flush_response_bytes(IntPtr pHttpContext, PFN_ASYNC_COMPLETION pfnCompletionCallback, IntPtr pvCompletionContext, out bool fCompletionExpected);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern HttpApi.HTTP_REQUEST_V2* http_get_raw_request(IntPtr pHttpContext);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern HttpApi.HTTP_RESPONSE_V2* http_get_raw_response(IntPtr pHttpContext);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern void http_set_response_status_code(IntPtr pHttpContext, ushort statusCode, byte* pszReason);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern int http_read_request_bytes(IntPtr pHttpContext, byte* pvBuffer, int cbBuffer, PFN_ASYNC_COMPLETION pfnCompletionCallback, IntPtr pvCompletionContext, out int dwBytesReceived, out bool fCompletionExpected);

        [DllImport("aspnetcore.dll")]
        public unsafe static extern bool http_get_completion_info(IntPtr pCompletionInfo, out int cbBytes, out int hr);
    }
}