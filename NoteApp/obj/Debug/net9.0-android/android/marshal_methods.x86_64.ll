; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [410 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [1230 x i64] [
	i64 u0x001e58127c546039, ; 0: lib_System.Globalization.dll.so => 42
	i64 u0x0024d0f62dee05bd, ; 1: Xamarin.KotlinX.Coroutines.Core.dll => 306
	i64 u0x0071cf2d27b7d61e, ; 2: lib_Xamarin.AndroidX.SwipeRefreshLayout.dll.so => 285
	i64 u0x01109b0e4d99e61f, ; 3: System.ComponentModel.Annotations.dll => 13
	i64 u0x01689251854dc4e9, ; 4: Microsoft.CodeAnalysis.Workspaces => 181
	i64 u0x01db22f5d13bd29b, ; 5: Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.dll => 182
	i64 u0x02123411c4e01926, ; 6: lib_Xamarin.AndroidX.Navigation.Runtime.dll.so => 274
	i64 u0x0284512fad379f7e, ; 7: System.Runtime.Handles => 105
	i64 u0x02a4c5a44384f885, ; 8: Microsoft.Extensions.Caching.Memory => 191
	i64 u0x02abedc11addc1ed, ; 9: lib_Mono.Android.Runtime.dll.so => 171
	i64 u0x02f55bf70672f5c8, ; 10: lib_System.IO.FileSystem.DriveInfo.dll.so => 48
	i64 u0x032267b2a94db371, ; 11: lib_Xamarin.AndroidX.AppCompat.dll.so => 228
	i64 u0x032344a7b9e98c5d, ; 12: ko/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 342
	i64 u0x033c3301d21c58c1, ; 13: pl/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 369
	i64 u0x03621c804933a890, ; 14: System.Buffers => 7
	i64 u0x0363ac97a4cb84e6, ; 15: SQLitePCLRaw.provider.e_sqlite3.dll => 212
	i64 u0x0399610510a38a38, ; 16: lib_System.Private.DataContractSerialization.dll.so => 86
	i64 u0x04263eac65fdd0c7, ; 17: lib-ko-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 368
	i64 u0x043032f1d071fae0, ; 18: ru/Microsoft.Maui.Controls.resources => 399
	i64 u0x044440a55165631e, ; 19: lib-cs-Microsoft.Maui.Controls.resources.dll.so => 377
	i64 u0x046eb1581a80c6b0, ; 20: vi/Microsoft.Maui.Controls.resources => 405
	i64 u0x047408741db2431a, ; 21: Xamarin.AndroidX.DynamicAnimation => 248
	i64 u0x0514f1a3ae77a228, ; 22: lib_Xamarin.Kotlin.StdLib.Common.dll.so => 302
	i64 u0x0517ef04e06e9f76, ; 23: System.Net.Primitives => 71
	i64 u0x0565d18c6da3de38, ; 24: Xamarin.AndroidX.RecyclerView => 278
	i64 u0x057bf9fa9fb09f7c, ; 25: Microsoft.Data.Sqlite.dll => 184
	i64 u0x0581db89237110e9, ; 26: lib_System.Collections.dll.so => 12
	i64 u0x05989cb940b225a9, ; 27: Microsoft.Maui.dll => 204
	i64 u0x05a1c25e78e22d87, ; 28: lib_System.Runtime.CompilerServices.Unsafe.dll.so => 102
	i64 u0x05ef98b6a1db882c, ; 29: lib_Microsoft.Data.Sqlite.dll.so => 184
	i64 u0x06076b5d2b581f08, ; 30: zh-HK/Microsoft.Maui.Controls.resources => 406
	i64 u0x06388ffe9f6c161a, ; 31: System.Xml.Linq.dll => 156
	i64 u0x063a29293b0d81df, ; 32: lib-ja-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 367
	i64 u0x06600c4c124cb358, ; 33: System.Configuration.dll => 19
	i64 u0x067f95c5ddab55b3, ; 34: lib_Xamarin.AndroidX.Fragment.Ktx.dll.so => 253
	i64 u0x0680a433c781bb3d, ; 35: Xamarin.AndroidX.Collection.Jvm => 235
	i64 u0x069fff96ec92a91d, ; 36: System.Xml.XPath.dll => 161
	i64 u0x06c665f3abc4d365, ; 37: lib_Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.dll.so => 182
	i64 u0x070b0847e18dab68, ; 38: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 250
	i64 u0x0739448d84d3b016, ; 39: lib_Xamarin.AndroidX.VectorDrawable.dll.so => 288
	i64 u0x07469f2eecce9e85, ; 40: mscorlib.dll => 167
	i64 u0x07c57877c7ba78ad, ; 41: ru/Microsoft.Maui.Controls.resources.dll => 399
	i64 u0x07dcdc7460a0c5e4, ; 42: System.Collections.NonGeneric => 10
	i64 u0x08122e52765333c8, ; 43: lib_Microsoft.Extensions.Logging.Debug.dll.so => 199
	i64 u0x0883f5fb92189b50, ; 44: ja/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 341
	i64 u0x088610fc2509f69e, ; 45: lib_Xamarin.AndroidX.VectorDrawable.Animated.dll.so => 289
	i64 u0x08a7c865576bbde7, ; 46: System.Reflection.Primitives => 96
	i64 u0x08c9d051a4a817e5, ; 47: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 245
	i64 u0x08f3c9788ee2153c, ; 48: Xamarin.AndroidX.DrawerLayout => 247
	i64 u0x09138715c92dba90, ; 49: lib_System.ComponentModel.Annotations.dll.so => 13
	i64 u0x0919c28b89381a0b, ; 50: lib_Microsoft.Extensions.Options.dll.so => 200
	i64 u0x092266563089ae3e, ; 51: lib_System.Collections.NonGeneric.dll.so => 10
	i64 u0x09ab38ad9baf7214, ; 52: lib-zh-Hant-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 361
	i64 u0x09d144a7e214d457, ; 53: System.Security.Cryptography => 127
	i64 u0x09e2b9f743db21a8, ; 54: lib_System.Reflection.Metadata.dll.so => 95
	i64 u0x0a805f95d98f597b, ; 55: lib_Microsoft.Extensions.Caching.Abstractions.dll.so => 190
	i64 u0x0abb3e2b271edc45, ; 56: System.Threading.Channels.dll => 140
	i64 u0x0b06b1feab070143, ; 57: System.Formats.Tar => 39
	i64 u0x0b3b632c3bbee20c, ; 58: sk/Microsoft.Maui.Controls.resources => 400
	i64 u0x0b6aff547b84fbe9, ; 59: Xamarin.KotlinX.Serialization.Core.Jvm => 309
	i64 u0x0bc0062e3ae6b583, ; 60: Microsoft.Build.Locator => 177
	i64 u0x0be2e1f8ce4064ed, ; 61: Xamarin.AndroidX.ViewPager => 291
	i64 u0x0c279376b1ae96ae, ; 62: lib_System.CodeDom.dll.so => 213
	i64 u0x0c36eea625afafdf, ; 63: ja/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 367
	i64 u0x0c3ca6cc978e2aae, ; 64: pt-BR/Microsoft.Maui.Controls.resources => 396
	i64 u0x0c59ad9fbbd43abe, ; 65: Mono.Android => 172
	i64 u0x0c65741e86371ee3, ; 66: lib_Xamarin.Android.Glide.GifDecoder.dll.so => 222
	i64 u0x0c74af560004e816, ; 67: Microsoft.Win32.Registry.dll => 5
	i64 u0x0c7790f60165fc06, ; 68: lib_Microsoft.Maui.Essentials.dll.so => 205
	i64 u0x0c83c82812e96127, ; 69: lib_System.Net.Mail.dll.so => 67
	i64 u0x0cce4bce83380b7f, ; 70: Xamarin.AndroidX.Security.SecurityCrypto => 282
	i64 u0x0cf6a95dadccbb9c, ; 71: zh-Hant/Microsoft.CodeAnalysis.resources.dll => 322
	i64 u0x0d13cd7cce4284e4, ; 72: System.Security.SecureString => 130
	i64 u0x0d3b5ab8b2766190, ; 73: lib_Microsoft.Bcl.AsyncInterfaces.dll.so => 176
	i64 u0x0d63f4f73521c24f, ; 74: lib_Xamarin.AndroidX.SavedState.SavedState.Ktx.dll.so => 281
	i64 u0x0e04e702012f8463, ; 75: Xamarin.AndroidX.Emoji2 => 249
	i64 u0x0e14e73a54dda68e, ; 76: lib_System.Net.NameResolution.dll.so => 68
	i64 u0x0e7acf675d09f75a, ; 77: it/Microsoft.CodeAnalysis.resources => 314
	i64 u0x0eba9c561385a823, ; 78: fr/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 339
	i64 u0x0ec47e16319c99d9, ; 79: lib-de-Microsoft.CodeAnalysis.resources.dll.so => 311
	i64 u0x0f37dd7a62ae99af, ; 80: lib_Xamarin.AndroidX.Collection.Ktx.dll.so => 236
	i64 u0x0f5e7abaa7cf470a, ; 81: System.Net.HttpListener => 66
	i64 u0x1001f97bbe242e64, ; 82: System.IO.UnmanagedMemoryStream => 57
	i64 u0x102861e4055f511a, ; 83: Microsoft.Bcl.AsyncInterfaces.dll => 176
	i64 u0x102a31b45304b1da, ; 84: Xamarin.AndroidX.CustomView => 244
	i64 u0x105b053cfbaba1f0, ; 85: lib_Microsoft.CodeAnalysis.dll.so => 178
	i64 u0x1065c4cb554c3d75, ; 86: System.IO.IsolatedStorage.dll => 52
	i64 u0x10a579e648829775, ; 87: Microsoft.CodeAnalysis => 178
	i64 u0x10f6cfcbcf801616, ; 88: System.IO.Compression.Brotli => 43
	i64 u0x114443cdcf2091f1, ; 89: System.Security.Cryptography.Primitives => 125
	i64 u0x114df3ff11650a65, ; 90: ru/Microsoft.CodeAnalysis.CSharp.resources => 332
	i64 u0x11a603952763e1d4, ; 91: System.Net.Mail => 67
	i64 u0x11a70d0e1009fb11, ; 92: System.Net.WebSockets.dll => 81
	i64 u0x11f26371eee0d3c1, ; 93: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll.so => 264
	i64 u0x1208da3842d90ff3, ; 94: lib-ko-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 329
	i64 u0x12128b3f59302d47, ; 95: lib_System.Xml.Serialization.dll.so => 158
	i64 u0x123639456fb056da, ; 96: System.Reflection.Emit.Lightweight.dll => 92
	i64 u0x12521e9764603eaa, ; 97: lib_System.Resources.Reader.dll.so => 99
	i64 u0x125b7f94acb989db, ; 98: Xamarin.AndroidX.RecyclerView.dll => 278
	i64 u0x12d3b63863d4ab0b, ; 99: lib_System.Threading.Overlapped.dll.so => 141
	i64 u0x131463e9417f52d4, ; 100: de/Microsoft.CodeAnalysis.CSharp.resources => 324
	i64 u0x134eab1061c395ee, ; 101: System.Transactions => 151
	i64 u0x138567fa954faa55, ; 102: Xamarin.AndroidX.Browser => 232
	i64 u0x1393617ead22674a, ; 103: zh-Hant/Microsoft.CodeAnalysis.resources => 322
	i64 u0x13a01de0cbc3f06c, ; 104: lib-fr-Microsoft.Maui.Controls.resources.dll.so => 383
	i64 u0x13beedefb0e28a45, ; 105: lib_System.Xml.XmlDocument.dll.so => 162
	i64 u0x13f1e5e209e91af4, ; 106: lib_Java.Interop.dll.so => 169
	i64 u0x13f1e880c25d96d1, ; 107: he/Microsoft.Maui.Controls.resources => 384
	i64 u0x143d8ea60a6a4011, ; 108: Microsoft.Extensions.DependencyInjection.Abstractions => 195
	i64 u0x1446c7a06695f3ea, ; 109: ko/Microsoft.CodeAnalysis.CSharp.resources.dll => 329
	i64 u0x1497051b917530bd, ; 110: lib_System.Net.WebSockets.dll.so => 81
	i64 u0x14e68447938213b7, ; 111: Xamarin.AndroidX.Collection.Ktx.dll => 236
	i64 u0x1506378c0000a92a, ; 112: lib-tr-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 333
	i64 u0x152a448bd1e745a7, ; 113: Microsoft.Win32.Primitives => 4
	i64 u0x1557de0138c445f4, ; 114: lib_Microsoft.Win32.Registry.dll.so => 5
	i64 u0x155943230c062553, ; 115: lib-zh-Hant-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 374
	i64 u0x15bdc156ed462f2f, ; 116: lib_System.IO.FileSystem.dll.so => 51
	i64 u0x15e300c2c1668655, ; 117: System.Resources.Writer.dll => 101
	i64 u0x16054fdcb6b3098b, ; 118: Microsoft.Extensions.DependencyModel.dll => 196
	i64 u0x16bf2a22df043a09, ; 119: System.IO.Pipes.dll => 56
	i64 u0x16ea2b318ad2d830, ; 120: System.Security.Cryptography.Algorithms => 120
	i64 u0x16eeae54c7ebcc08, ; 121: System.Reflection.dll => 98
	i64 u0x17125c9a85b4929f, ; 122: lib_netstandard.dll.so => 168
	i64 u0x1716866f7416792e, ; 123: lib_System.Security.AccessControl.dll.so => 118
	i64 u0x174f71c46216e44a, ; 124: Xamarin.KotlinX.Coroutines.Core => 306
	i64 u0x1752c12f1e1fc00c, ; 125: System.Core => 21
	i64 u0x17976319373fd889, ; 126: cs/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 336
	i64 u0x17b56e25558a5d36, ; 127: lib-hu-Microsoft.Maui.Controls.resources.dll.so => 387
	i64 u0x17f9358913beb16a, ; 128: System.Text.Encodings.Web => 137
	i64 u0x18032a868e0c3843, ; 129: de/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 363
	i64 u0x1809fb23f29ba44a, ; 130: lib_System.Reflection.TypeExtensions.dll.so => 97
	i64 u0x18402a709e357f3b, ; 131: lib_Xamarin.KotlinX.Serialization.Core.Jvm.dll.so => 309
	i64 u0x18950fae1c2bc98e, ; 132: lib-cs-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 323
	i64 u0x18a9befae51bb361, ; 133: System.Net.WebClient => 77
	i64 u0x18f0ce884e87d89a, ; 134: nb/Microsoft.Maui.Controls.resources.dll => 393
	i64 u0x192712eaa333180f, ; 135: lib-zh-Hant-Microsoft.CodeAnalysis.resources.dll.so => 322
	i64 u0x193d7a04b7eda8bc, ; 136: lib_Xamarin.AndroidX.Print.dll.so => 276
	i64 u0x194dc72e14e1bd09, ; 137: de/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 337
	i64 u0x19777fba3c41b398, ; 138: Xamarin.AndroidX.Startup.StartupRuntime.dll => 284
	i64 u0x19a4c090f14ebb66, ; 139: System.Security.Claims => 119
	i64 u0x1a761daba47c6ad5, ; 140: ja/Microsoft.CodeAnalysis.resources.dll => 315
	i64 u0x1a91866a319e9259, ; 141: lib_System.Collections.Concurrent.dll.so => 8
	i64 u0x1a9e139e4762aaf8, ; 142: es/Microsoft.CodeAnalysis.CSharp.resources.dll => 325
	i64 u0x1aac34d1917ba5d3, ; 143: lib_System.dll.so => 165
	i64 u0x1aad60783ffa3e5b, ; 144: lib-th-Microsoft.Maui.Controls.resources.dll.so => 402
	i64 u0x1aea8f1c3b282172, ; 145: lib_System.Net.Ping.dll.so => 70
	i64 u0x1b4b7a1d0d265fa2, ; 146: Xamarin.Android.Glide.DiskLruCache => 221
	i64 u0x1b50378f54aa4b01, ; 147: pt-BR/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 370
	i64 u0x1bbdb16cfa73e785, ; 148: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android => 265
	i64 u0x1bc766e07b2b4241, ; 149: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 279
	i64 u0x1c074bdeeae2e1c9, ; 150: lib-pl-Microsoft.CodeAnalysis.resources.dll.so => 317
	i64 u0x1c753b5ff15bce1b, ; 151: Mono.Android.Runtime.dll => 171
	i64 u0x1cce6e800e06bdb7, ; 152: zh-Hans/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 373
	i64 u0x1cd47467799d8250, ; 153: System.Threading.Tasks.dll => 145
	i64 u0x1d23eafdc6dc346c, ; 154: System.Globalization.Calendars.dll => 40
	i64 u0x1d68fe2a371ca539, ; 155: zh-Hans/Microsoft.CodeAnalysis.Workspaces.resources.dll => 360
	i64 u0x1da4110562816681, ; 156: Xamarin.AndroidX.Security.SecurityCrypto.dll => 282
	i64 u0x1db6820994506bf5, ; 157: System.IO.FileSystem.AccessControl.dll => 47
	i64 u0x1dbb0c2c6a999acb, ; 158: System.Diagnostics.StackTrace => 30
	i64 u0x1e3d87657e9659bc, ; 159: Xamarin.AndroidX.Navigation.UI => 275
	i64 u0x1e71143913d56c10, ; 160: lib-ko-Microsoft.Maui.Controls.resources.dll.so => 391
	i64 u0x1e7c31185e2fb266, ; 161: lib_System.Threading.Tasks.Parallel.dll.so => 144
	i64 u0x1ed8fcce5e9b50a0, ; 162: Microsoft.Extensions.Options.dll => 200
	i64 u0x1edf56c90236be4d, ; 163: ru/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 371
	i64 u0x1f055d15d807e1b2, ; 164: System.Xml.XmlSerializer => 163
	i64 u0x1f1ed22c1085f044, ; 165: lib_System.Diagnostics.FileVersionInfo.dll.so => 28
	i64 u0x1f2c5edaae56f4c2, ; 166: tr/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 346
	i64 u0x1f61df9c5b94d2c1, ; 167: lib_System.Numerics.dll.so => 84
	i64 u0x1f750bb5421397de, ; 168: lib_Xamarin.AndroidX.Tracing.Tracing.dll.so => 286
	i64 u0x1fe22396eed9deb5, ; 169: lib-pl-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 356
	i64 u0x20237ea48006d7a8, ; 170: lib_System.Net.WebClient.dll.so => 77
	i64 u0x209375905fcc1bad, ; 171: lib_System.IO.Compression.Brotli.dll.so => 43
	i64 u0x20fab3cf2dfbc8df, ; 172: lib_System.Diagnostics.Process.dll.so => 29
	i64 u0x2110167c128cba15, ; 173: System.Globalization => 42
	i64 u0x21419508838f7547, ; 174: System.Runtime.CompilerServices.VisualC => 103
	i64 u0x2174319c0d835bc9, ; 175: System.Runtime => 117
	i64 u0x2198e5bc8b7153fa, ; 176: Xamarin.AndroidX.Annotation.Experimental.dll => 226
	i64 u0x219ea1b751a4dee4, ; 177: lib_System.IO.Compression.ZipFile.dll.so => 45
	i64 u0x21cc7e445dcd5469, ; 178: System.Reflection.Emit.ILGeneration => 91
	i64 u0x220fd4f2e7c48170, ; 179: th/Microsoft.Maui.Controls.resources => 402
	i64 u0x224538d85ed15a82, ; 180: System.IO.Pipes => 56
	i64 u0x22908438c6bed1af, ; 181: lib_System.Threading.Timer.dll.so => 148
	i64 u0x237be844f1f812c7, ; 182: System.Threading.Thread.dll => 146
	i64 u0x23807c59646ec4f3, ; 183: lib_Microsoft.EntityFrameworkCore.dll.so => 185
	i64 u0x23852b3bdc9f7096, ; 184: System.Resources.ResourceManager => 100
	i64 u0x23986dd7e5d4fc01, ; 185: System.IO.FileSystem.Primitives.dll => 49
	i64 u0x2407aef2bbe8fadf, ; 186: System.Console => 20
	i64 u0x240abe014b27e7d3, ; 187: Xamarin.AndroidX.Core.dll => 241
	i64 u0x245ebc45bf698558, ; 188: ru/Microsoft.CodeAnalysis.resources.dll => 319
	i64 u0x247619fe4413f8bf, ; 189: System.Runtime.Serialization.Primitives.dll => 114
	i64 u0x24a5c479679dfcd0, ; 190: ru/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 371
	i64 u0x24cbd781fb976f7f, ; 191: lib_Microsoft.CodeAnalysis.Workspaces.MSBuild.dll.so => 183
	i64 u0x24de8d301281575e, ; 192: Xamarin.Android.Glide => 219
	i64 u0x252073cc3caa62c2, ; 193: fr/Microsoft.Maui.Controls.resources.dll => 383
	i64 u0x256b8d41255f01b1, ; 194: Xamarin.Google.Crypto.Tink.Android => 297
	i64 u0x25a0a7eff76ea08e, ; 195: SQLitePCLRaw.batteries_v2.dll => 209
	i64 u0x25dbdf9c11e7095a, ; 196: cs/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 362
	i64 u0x2626d536c88621f2, ; 197: lib-ko-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 342
	i64 u0x2662c629b96b0b30, ; 198: lib_Xamarin.Kotlin.StdLib.dll.so => 301
	i64 u0x268c1439f13bcc29, ; 199: lib_Microsoft.Extensions.Primitives.dll.so => 201
	i64 u0x26a670e154a9c54b, ; 200: System.Reflection.Extensions.dll => 94
	i64 u0x26d077d9678fe34f, ; 201: System.IO.dll => 58
	i64 u0x272377f9edc266a2, ; 202: tr/Microsoft.CodeAnalysis.resources => 320
	i64 u0x273f3515de5faf0d, ; 203: id/Microsoft.Maui.Controls.resources.dll => 388
	i64 u0x2742545f9094896d, ; 204: hr/Microsoft.Maui.Controls.resources => 386
	i64 u0x2759af78ab94d39b, ; 205: System.Net.WebSockets => 81
	i64 u0x2760ac2972e51bf5, ; 206: lib-cs-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 336
	i64 u0x27b2b16f3e9de038, ; 207: Xamarin.Google.Crypto.Tink.Android.dll => 297
	i64 u0x27b410442fad6cf1, ; 208: Java.Interop.dll => 169
	i64 u0x27b97e0d52c3034a, ; 209: System.Diagnostics.Debug => 26
	i64 u0x2801845a2c71fbfb, ; 210: System.Net.Primitives.dll => 71
	i64 u0x286835e259162700, ; 211: lib_Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll.so => 277
	i64 u0x2949f3617a02c6b2, ; 212: Xamarin.AndroidX.ExifInterface => 251
	i64 u0x29e4f22f4ae1c7db, ; 213: pl/Microsoft.CodeAnalysis.Workspaces.resources => 356
	i64 u0x2a128783efe70ba0, ; 214: uk/Microsoft.Maui.Controls.resources.dll => 404
	i64 u0x2a3b095612184159, ; 215: lib_System.Net.NetworkInformation.dll.so => 69
	i64 u0x2a6507a5ffabdf28, ; 216: System.Diagnostics.TraceSource.dll => 33
	i64 u0x2ad156c8e1354139, ; 217: fi/Microsoft.Maui.Controls.resources => 382
	i64 u0x2ad5d6b13b7a3e04, ; 218: System.ComponentModel.DataAnnotations.dll => 14
	i64 u0x2af298f63581d886, ; 219: System.Text.RegularExpressions.dll => 139
	i64 u0x2afc1c4f898552ee, ; 220: lib_System.Formats.Asn1.dll.so => 38
	i64 u0x2b148910ed40fbf9, ; 221: zh-Hant/Microsoft.Maui.Controls.resources.dll => 408
	i64 u0x2b2afdca57bcee98, ; 222: Microsoft.Build.Locator.dll => 177
	i64 u0x2b6989d78cba9a15, ; 223: Xamarin.AndroidX.Concurrent.Futures.dll => 237
	i64 u0x2bf0406a075bdf30, ; 224: ja/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 367
	i64 u0x2c8bd14bb93a7d82, ; 225: lib-pl-Microsoft.Maui.Controls.resources.dll.so => 395
	i64 u0x2cbd9262ca785540, ; 226: lib_System.Text.Encoding.CodePages.dll.so => 134
	i64 u0x2cc9e1fed6257257, ; 227: lib_System.Reflection.Emit.Lightweight.dll.so => 92
	i64 u0x2cd723e9fe623c7c, ; 228: lib_System.Private.Xml.Linq.dll.so => 88
	i64 u0x2ced40c2c73942d4, ; 229: lib-tr-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 372
	i64 u0x2d169d318a968379, ; 230: System.Threading.dll => 149
	i64 u0x2d47774b7d993f59, ; 231: sv/Microsoft.Maui.Controls.resources.dll => 401
	i64 u0x2d5ffcae1ad0aaca, ; 232: System.Data.dll => 24
	i64 u0x2d6295bc2ab86a27, ; 233: ja/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 341
	i64 u0x2db915caf23548d2, ; 234: System.Text.Json.dll => 138
	i64 u0x2dcaa0bb15a4117a, ; 235: System.IO.UnmanagedMemoryStream.dll => 57
	i64 u0x2e4d2e03e610a6e9, ; 236: pl/Microsoft.CodeAnalysis.resources => 317
	i64 u0x2e5a40c319acb800, ; 237: System.IO.FileSystem => 51
	i64 u0x2e6f1f226821322a, ; 238: el/Microsoft.Maui.Controls.resources.dll => 380
	i64 u0x2f02f94df3200fe5, ; 239: System.Diagnostics.Process => 29
	i64 u0x2f2e98e1c89b1aff, ; 240: System.Xml.ReaderWriter => 157
	i64 u0x2f5911d9ba814e4e, ; 241: System.Diagnostics.Tracing => 34
	i64 u0x2f84070a459bc31f, ; 242: lib_System.Xml.dll.so => 164
	i64 u0x2feb4d2fcda05cfd, ; 243: Microsoft.Extensions.Caching.Abstractions.dll => 190
	i64 u0x309ee9eeec09a71e, ; 244: lib_Xamarin.AndroidX.Fragment.dll.so => 252
	i64 u0x30c6dda129408828, ; 245: System.IO.IsolatedStorage => 52
	i64 u0x30dbb00aded5b4cb, ; 246: ko/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 368
	i64 u0x31195fef5d8fb552, ; 247: _Microsoft.Android.Resource.Designer.dll => 409
	i64 u0x312c8ed623cbfc8d, ; 248: Xamarin.AndroidX.Window.dll => 293
	i64 u0x31496b779ed0663d, ; 249: lib_System.Reflection.DispatchProxy.dll.so => 90
	i64 u0x32243413e774362a, ; 250: Xamarin.AndroidX.CardView.dll => 233
	i64 u0x3235427f8d12dae1, ; 251: lib_System.Drawing.Primitives.dll.so => 35
	i64 u0x324622a9fd95b0c8, ; 252: lib-cs-Microsoft.CodeAnalysis.resources.dll.so => 310
	i64 u0x329753a17a517811, ; 253: fr/Microsoft.Maui.Controls.resources => 383
	i64 u0x32aa989ff07a84ff, ; 254: lib_System.Xml.ReaderWriter.dll.so => 157
	i64 u0x32e787fbcf22bfdb, ; 255: fr/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 365
	i64 u0x33829542f112d59b, ; 256: System.Collections.Immutable => 9
	i64 u0x33a31443733849fe, ; 257: lib-es-Microsoft.Maui.Controls.resources.dll.so => 381
	i64 u0x33e03d7b100711f1, ; 258: zh-Hans/Microsoft.CodeAnalysis.Workspaces.resources => 360
	i64 u0x341abc357fbb4ebf, ; 259: lib_System.Net.Sockets.dll.so => 76
	i64 u0x3496c1e2dcaf5ecc, ; 260: lib_System.IO.Pipes.AccessControl.dll.so => 55
	i64 u0x34d10ee19592dc34, ; 261: de/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 363
	i64 u0x34dfd74fe2afcf37, ; 262: Microsoft.Maui => 204
	i64 u0x34e292762d9615df, ; 263: cs/Microsoft.Maui.Controls.resources.dll => 377
	i64 u0x34ef56e1435b2843, ; 264: pl/Microsoft.CodeAnalysis.CSharp.resources.dll => 330
	i64 u0x3508234247f48404, ; 265: Microsoft.Maui.Controls => 202
	i64 u0x353590da528c9d22, ; 266: System.ComponentModel.Annotations => 13
	i64 u0x3549870798b4cd30, ; 267: lib_Xamarin.AndroidX.ViewPager2.dll.so => 292
	i64 u0x355282fc1c909694, ; 268: Microsoft.Extensions.Configuration => 192
	i64 u0x3552fc5d578f0fbf, ; 269: Xamarin.AndroidX.Arch.Core.Common => 230
	i64 u0x355c649948d55d97, ; 270: lib_System.Runtime.Intrinsics.dll.so => 109
	i64 u0x356653662ca42eb1, ; 271: lib-it-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 353
	i64 u0x35766456ffb7a7b4, ; 272: fr/Microsoft.CodeAnalysis.CSharp.resources.dll => 326
	i64 u0x35bf814e2d496b74, ; 273: lib_Microsoft.CodeAnalysis.Workspaces.dll.so => 181
	i64 u0x35ea9d1c6834bc8c, ; 274: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 268
	i64 u0x3628ab68db23a01a, ; 275: lib_System.Diagnostics.Tools.dll.so => 32
	i64 u0x3673b042508f5b6b, ; 276: lib_System.Runtime.Extensions.dll.so => 104
	i64 u0x36740f1a8ecdc6c4, ; 277: System.Numerics => 84
	i64 u0x36b2b50fdf589ae2, ; 278: System.Reflection.Emit.Lightweight => 92
	i64 u0x36cada77dc79928b, ; 279: System.IO.MemoryMappedFiles => 53
	i64 u0x374ef46b06791af6, ; 280: System.Reflection.Primitives.dll => 96
	i64 u0x376bf93e521a5417, ; 281: lib_Xamarin.Jetbrains.Annotations.dll.so => 300
	i64 u0x37bc29f3183003b6, ; 282: lib_System.IO.dll.so => 58
	i64 u0x380134e03b1e160a, ; 283: System.Collections.Immutable.dll => 9
	i64 u0x38049b5c59b39324, ; 284: System.Runtime.CompilerServices.Unsafe => 102
	i64 u0x38143d85e217351a, ; 285: System.Composition.Hosting => 216
	i64 u0x3837e860635e56ed, ; 286: it/Microsoft.CodeAnalysis.Workspaces.resources => 353
	i64 u0x3843b9508197bc53, ; 287: pt-BR/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 344
	i64 u0x385c17636bb6fe6e, ; 288: Xamarin.AndroidX.CustomView.dll => 244
	i64 u0x38869c811d74050e, ; 289: System.Net.NameResolution.dll => 68
	i64 u0x393c226616977fdb, ; 290: lib_Xamarin.AndroidX.ViewPager.dll.so => 291
	i64 u0x395e37c3334cf82a, ; 291: lib-ca-Microsoft.Maui.Controls.resources.dll.so => 376
	i64 u0x39eb5ad7e3b83323, ; 292: fr/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 339
	i64 u0x3ab5859054645f72, ; 293: System.Security.Cryptography.Primitives.dll => 125
	i64 u0x3ad75090c3fac0e9, ; 294: lib_Xamarin.AndroidX.ResourceInspection.Annotation.dll.so => 279
	i64 u0x3ae44ac43a1fbdbb, ; 295: System.Runtime.Serialization => 116
	i64 u0x3b519320d3a43198, ; 296: lib-ko-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 355
	i64 u0x3b860f9932505633, ; 297: lib_System.Text.Encoding.Extensions.dll.so => 135
	i64 u0x3c3aafb6b3a00bf6, ; 298: lib_System.Security.Cryptography.X509Certificates.dll.so => 126
	i64 u0x3c4049146b59aa90, ; 299: System.Runtime.InteropServices.JavaScript => 106
	i64 u0x3c7c495f58ac5ee9, ; 300: Xamarin.Kotlin.StdLib => 301
	i64 u0x3c7e5ed3d5db71bb, ; 301: System.Security => 131
	i64 u0x3cd9d281d402eb9b, ; 302: Xamarin.AndroidX.Browser.dll => 232
	i64 u0x3d1c50cc001a991e, ; 303: Xamarin.Google.Guava.ListenableFuture.dll => 299
	i64 u0x3d2b1913edfc08d7, ; 304: lib_System.Threading.ThreadPool.dll.so => 147
	i64 u0x3d46f0b995082740, ; 305: System.Xml.Linq => 156
	i64 u0x3d8a8f400514a790, ; 306: Xamarin.AndroidX.Fragment.Ktx.dll => 253
	i64 u0x3d9c2a242b040a50, ; 307: lib_Xamarin.AndroidX.Core.dll.so => 241
	i64 u0x3da7781d6333a8fe, ; 308: SQLitePCLRaw.batteries_v2 => 209
	i64 u0x3dbb6b9f5ab90fa7, ; 309: lib_Xamarin.AndroidX.DynamicAnimation.dll.so => 248
	i64 u0x3e5441657549b213, ; 310: Xamarin.AndroidX.ResourceInspection.Annotation => 279
	i64 u0x3e57d4d195c53c2e, ; 311: System.Reflection.TypeExtensions => 97
	i64 u0x3e616ab4ed1f3f15, ; 312: lib_System.Data.dll.so => 24
	i64 u0x3f1d226e6e06db7e, ; 313: Xamarin.AndroidX.SlidingPaneLayout.dll => 283
	i64 u0x3f510adf788828dd, ; 314: System.Threading.Tasks.Extensions => 143
	i64 u0x3f9488d1edef88fe, ; 315: lib-pt-BR-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 344
	i64 u0x407740ff2e914d86, ; 316: Xamarin.AndroidX.Print.dll => 276
	i64 u0x407a10bb4bf95829, ; 317: lib_Xamarin.AndroidX.Navigation.Common.dll.so => 272
	i64 u0x40c98b6bd77346d4, ; 318: Microsoft.VisualBasic.dll => 3
	i64 u0x415c502eb40e7418, ; 319: es/Microsoft.CodeAnalysis.resources.dll => 312
	i64 u0x41833cf766d27d96, ; 320: mscorlib => 167
	i64 u0x41cab042be111c34, ; 321: lib_Xamarin.AndroidX.AppCompat.AppCompatResources.dll.so => 229
	i64 u0x423a9ecc4d905a88, ; 322: lib_System.Resources.ResourceManager.dll.so => 100
	i64 u0x423bf51ae7def810, ; 323: System.Xml.XPath => 161
	i64 u0x42462ff15ddba223, ; 324: System.Resources.Reader.dll => 99
	i64 u0x42a31b86e6ccc3f0, ; 325: System.Diagnostics.Contracts => 25
	i64 u0x430e95b891249788, ; 326: lib_System.Reflection.Emit.dll.so => 93
	i64 u0x43375950ec7c1b6a, ; 327: netstandard.dll => 168
	i64 u0x434c4e1d9284cdae, ; 328: Mono.Android.dll => 172
	i64 u0x43505013578652a0, ; 329: lib_Xamarin.AndroidX.Activity.Ktx.dll.so => 224
	i64 u0x437d06c381ed575a, ; 330: lib_Microsoft.VisualBasic.dll.so => 3
	i64 u0x43950f84de7cc79a, ; 331: pl/Microsoft.Maui.Controls.resources.dll => 395
	i64 u0x43e8ca5bc927ff37, ; 332: lib_Xamarin.AndroidX.Emoji2.ViewsHelper.dll.so => 250
	i64 u0x448bd33429269b19, ; 333: Microsoft.CSharp => 1
	i64 u0x4499fa3c8e494654, ; 334: lib_System.Runtime.Serialization.Primitives.dll.so => 114
	i64 u0x4515080865a951a5, ; 335: Xamarin.Kotlin.StdLib.dll => 301
	i64 u0x453c1277f85cf368, ; 336: lib_Microsoft.EntityFrameworkCore.Abstractions.dll.so => 186
	i64 u0x4545802489b736b9, ; 337: Xamarin.AndroidX.Fragment.Ktx => 253
	i64 u0x454b4d1e66bb783c, ; 338: Xamarin.AndroidX.Lifecycle.Process => 261
	i64 u0x45c40276a42e283e, ; 339: System.Diagnostics.TraceSource => 33
	i64 u0x45d443f2a29adc37, ; 340: System.AppContext.dll => 6
	i64 u0x45fcc9fd66f25095, ; 341: Microsoft.Extensions.DependencyModel => 196
	i64 u0x4645b3d617ebf34b, ; 342: lib-cs-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 362
	i64 u0x46a4213bc97fe5ae, ; 343: lib-ru-Microsoft.Maui.Controls.resources.dll.so => 399
	i64 u0x46ca22193958c24b, ; 344: Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost => 182
	i64 u0x47358bd471172e1d, ; 345: lib_System.Xml.Linq.dll.so => 156
	i64 u0x475461b41cd2bae5, ; 346: lib-zh-Hant-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 335
	i64 u0x47daf4e1afbada10, ; 347: pt/Microsoft.Maui.Controls.resources => 397
	i64 u0x480c0a47dd42dd81, ; 348: lib_System.IO.MemoryMappedFiles.dll.so => 53
	i64 u0x4843e6c1ee585264, ; 349: Microsoft.EntityFrameworkCore.Design.dll => 187
	i64 u0x488d293220a4fe37, ; 350: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 255
	i64 u0x48d8ed46e9461716, ; 351: es/Microsoft.CodeAnalysis.Workspaces.resources => 351
	i64 u0x49e952f19a4e2022, ; 352: System.ObjectModel => 85
	i64 u0x49f9e6948a8131e4, ; 353: lib_Xamarin.AndroidX.VersionedParcelable.dll.so => 290
	i64 u0x4a1afd3bf9c69c98, ; 354: fr/Microsoft.CodeAnalysis.resources => 313
	i64 u0x4a4f1047df83044b, ; 355: lib_System.Composition.AttributedModel.dll.so => 214
	i64 u0x4a5667b2462a664b, ; 356: lib_Xamarin.AndroidX.Navigation.UI.dll.so => 275
	i64 u0x4a59e8951c30f637, ; 357: lib-zh-Hans-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 360
	i64 u0x4a7a18981dbd56bc, ; 358: System.IO.Compression.FileSystem.dll => 44
	i64 u0x4aa5c60350917c06, ; 359: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll.so => 260
	i64 u0x4b07a0ed0ab33ff4, ; 360: System.Runtime.Extensions.dll => 104
	i64 u0x4b2c56ec7a03ca88, ; 361: lib-ja-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 354
	i64 u0x4b484a0d637947d7, ; 362: lib-zh-Hans-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 334
	i64 u0x4b558744a6e1abe0, ; 363: lib-de-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 324
	i64 u0x4b576d47ac054f3c, ; 364: System.IO.FileSystem.AccessControl => 47
	i64 u0x4b7b6532ded934b7, ; 365: System.Text.Json => 138
	i64 u0x4c7755cf07ad2d5f, ; 366: System.Net.Http.Json.dll => 64
	i64 u0x4ca014ceac582c86, ; 367: Microsoft.EntityFrameworkCore.Relational.dll => 188
	i64 u0x4cb66d7fdf45d66b, ; 368: ko/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 342
	i64 u0x4cc5f15266470798, ; 369: lib_Xamarin.AndroidX.Loader.dll.so => 270
	i64 u0x4cf6f67dc77aacd2, ; 370: System.Net.NetworkInformation.dll => 69
	i64 u0x4d3183dd245425d4, ; 371: System.Net.WebSockets.Client.dll => 80
	i64 u0x4d479f968a05e504, ; 372: System.Linq.Expressions.dll => 59
	i64 u0x4d55a010ffc4faff, ; 373: System.Private.Xml => 89
	i64 u0x4d5cbe77561c5b2e, ; 374: System.Web.dll => 154
	i64 u0x4d77512dbd86ee4c, ; 375: lib_Xamarin.AndroidX.Arch.Core.Common.dll.so => 230
	i64 u0x4d7793536e79c309, ; 376: System.ServiceProcess => 133
	i64 u0x4d95fccc1f67c7ca, ; 377: System.Runtime.Loader.dll => 110
	i64 u0x4dcf44c3c9b076a2, ; 378: it/Microsoft.Maui.Controls.resources.dll => 389
	i64 u0x4dd35d8b99c17bef, ; 379: zh-Hans/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 373
	i64 u0x4dd9247f1d2c3235, ; 380: Xamarin.AndroidX.Loader.dll => 270
	i64 u0x4e0118d7e6df6ed3, ; 381: lib-zh-Hans-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 347
	i64 u0x4e2aeee78e2c4a87, ; 382: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 277
	i64 u0x4e32f00cb0937401, ; 383: Mono.Android.Runtime => 171
	i64 u0x4e5eea4668ac2b18, ; 384: System.Text.Encoding.CodePages => 134
	i64 u0x4e84220084ab2d20, ; 385: cs/Microsoft.CodeAnalysis.CSharp.resources.dll => 323
	i64 u0x4ebd0c4b82c5eefc, ; 386: lib_System.Threading.Channels.dll.so => 140
	i64 u0x4ee8eaa9c9c1151a, ; 387: System.Globalization.Calendars => 40
	i64 u0x4f21ee6ef9eb527e, ; 388: ca/Microsoft.Maui.Controls.resources => 376
	i64 u0x4f395cbd2708b3c5, ; 389: ru/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 345
	i64 u0x4f7ed4233b906e51, ; 390: cs/Microsoft.CodeAnalysis.Workspaces.resources => 349
	i64 u0x4fd5f3ee53d0a4f0, ; 391: SQLitePCLRaw.lib.e_sqlite3.android => 211
	i64 u0x5037f0be3c28c7a3, ; 392: lib_Microsoft.Maui.Controls.dll.so => 202
	i64 u0x50c3a29b21050d45, ; 393: System.Linq.Parallel.dll => 60
	i64 u0x5131bbe80989093f, ; 394: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 267
	i64 u0x516324a5050a7e3c, ; 395: System.Net.WebProxy => 79
	i64 u0x516d6f0b21a303de, ; 396: lib_System.Diagnostics.Contracts.dll.so => 25
	i64 u0x51bb8a2afe774e32, ; 397: System.Drawing => 36
	i64 u0x5247c5c32a4140f0, ; 398: System.Resources.Reader => 99
	i64 u0x526bb15e3c386364, ; 399: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 264
	i64 u0x526ce79eb8e90527, ; 400: lib_System.Net.Primitives.dll.so => 71
	i64 u0x52829f00b4467c38, ; 401: lib_System.Data.Common.dll.so => 22
	i64 u0x529ffe06f39ab8db, ; 402: Xamarin.AndroidX.Core => 241
	i64 u0x52ff996554dbf352, ; 403: Microsoft.Maui.Graphics => 206
	i64 u0x533514f6711b299b, ; 404: ko/Microsoft.CodeAnalysis.CSharp.resources => 329
	i64 u0x535f7e40e8fef8af, ; 405: lib-sk-Microsoft.Maui.Controls.resources.dll.so => 400
	i64 u0x53978aac584c666e, ; 406: lib_System.Security.Cryptography.Cng.dll.so => 121
	i64 u0x53a96d5c86c9e194, ; 407: System.Net.NetworkInformation => 69
	i64 u0x53be1038a61e8d44, ; 408: System.Runtime.InteropServices.RuntimeInformation.dll => 107
	i64 u0x53c3014b9437e684, ; 409: lib-zh-HK-Microsoft.Maui.Controls.resources.dll.so => 406
	i64 u0x53e450ebd586f842, ; 410: lib_Xamarin.AndroidX.LocalBroadcastManager.dll.so => 271
	i64 u0x5435e6f049e9bc37, ; 411: System.Security.Claims.dll => 119
	i64 u0x54795225dd1587af, ; 412: lib_System.Runtime.dll.so => 117
	i64 u0x547a34f14e5f6210, ; 413: Xamarin.AndroidX.Lifecycle.Common.dll => 256
	i64 u0x54d75f85d6578cff, ; 414: lib-fr-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 326
	i64 u0x556e8b63b660ab8b, ; 415: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 257
	i64 u0x5588627c9a108ec9, ; 416: System.Collections.Specialized => 11
	i64 u0x55a898e4f42e3fae, ; 417: Microsoft.VisualBasic.Core.dll => 2
	i64 u0x55fa0c610fe93bb1, ; 418: lib_System.Security.Cryptography.OpenSsl.dll.so => 124
	i64 u0x56442b99bc64bb47, ; 419: System.Runtime.Serialization.Xml.dll => 115
	i64 u0x56a8b26e1aeae27b, ; 420: System.Threading.Tasks.Dataflow => 142
	i64 u0x56f932d61e93c07f, ; 421: System.Globalization.Extensions => 41
	i64 u0x571c5cfbec5ae8e2, ; 422: System.Private.Uri => 87
	i64 u0x5724fbe6b45b7f07, ; 423: lib-pt-BR-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 331
	i64 u0x5759c386703a58a0, ; 424: fr/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 365
	i64 u0x57623b72b8f4cc3f, ; 425: ko/Microsoft.CodeAnalysis.Workspaces.resources.dll => 355
	i64 u0x576499c9f52fea31, ; 426: Xamarin.AndroidX.Annotation => 225
	i64 u0x578cd35c91d7b347, ; 427: lib_SQLitePCLRaw.core.dll.so => 210
	i64 u0x579a06fed6eec900, ; 428: System.Private.CoreLib.dll => 173
	i64 u0x57c542c14049b66d, ; 429: System.Diagnostics.DiagnosticSource => 27
	i64 u0x581a8bd5cfda563e, ; 430: System.Threading.Timer => 148
	i64 u0x582e758eda676c85, ; 431: zh-Hant/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 348
	i64 u0x58601b2dda4a27b9, ; 432: lib-ja-Microsoft.Maui.Controls.resources.dll.so => 390
	i64 u0x58688d9af496b168, ; 433: Microsoft.Extensions.DependencyInjection.dll => 194
	i64 u0x588c167a79db6bfb, ; 434: lib_Xamarin.Google.ErrorProne.Annotations.dll.so => 298
	i64 u0x58ef0576630aa114, ; 435: fr/Microsoft.CodeAnalysis.CSharp.resources => 326
	i64 u0x5906028ae5151104, ; 436: Xamarin.AndroidX.Activity.Ktx => 224
	i64 u0x595a356d23e8da9a, ; 437: lib_Microsoft.CSharp.dll.so => 1
	i64 u0x597d58a5c4373cea, ; 438: System.Composition.Runtime.dll => 217
	i64 u0x59f9e60b9475085f, ; 439: lib_Xamarin.AndroidX.Annotation.Experimental.dll.so => 226
	i64 u0x5a745f5101a75527, ; 440: lib_System.IO.Compression.FileSystem.dll.so => 44
	i64 u0x5a89a886ae30258d, ; 441: lib_Xamarin.AndroidX.CoordinatorLayout.dll.so => 240
	i64 u0x5a8f6699f4a1caa9, ; 442: lib_System.Threading.dll.so => 149
	i64 u0x5ae8e4f3eae4d547, ; 443: Xamarin.AndroidX.Legacy.Support.Core.Utils => 255
	i64 u0x5ae9cd33b15841bf, ; 444: System.ComponentModel => 18
	i64 u0x5b54391bdc6fcfe6, ; 445: System.Private.DataContractSerialization => 86
	i64 u0x5b5f0e240a06a2a2, ; 446: da/Microsoft.Maui.Controls.resources.dll => 378
	i64 u0x5b8109e8e14c5e3e, ; 447: System.Globalization.Extensions.dll => 41
	i64 u0x5ba42c66b858352a, ; 448: ko/Microsoft.CodeAnalysis.Workspaces.resources => 355
	i64 u0x5bb93c3ef9525c89, ; 449: es/Microsoft.CodeAnalysis.resources => 312
	i64 u0x5bddd04d72a9e350, ; 450: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 260
	i64 u0x5bdf16b09da116ab, ; 451: Xamarin.AndroidX.Collection => 234
	i64 u0x5be34cb3cc2ff949, ; 452: tr/Microsoft.CodeAnalysis.CSharp.resources => 333
	i64 u0x5c019d5266093159, ; 453: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll.so => 265
	i64 u0x5c30a4a35f9cc8c4, ; 454: lib_System.Reflection.Extensions.dll.so => 94
	i64 u0x5c393624b8176517, ; 455: lib_Microsoft.Extensions.Logging.dll.so => 197
	i64 u0x5c53c29f5073b0c9, ; 456: System.Diagnostics.FileVersionInfo => 28
	i64 u0x5c6724284a5e7317, ; 457: lib-tr-Microsoft.CodeAnalysis.resources.dll.so => 320
	i64 u0x5c87463c575c7616, ; 458: lib_System.Globalization.Extensions.dll.so => 41
	i64 u0x5d0233e3983e2c1c, ; 459: zh-Hans/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 347
	i64 u0x5d0a4a29b02d9d3c, ; 460: System.Net.WebHeaderCollection.dll => 78
	i64 u0x5d40c9b15181641f, ; 461: lib_Xamarin.AndroidX.Emoji2.dll.so => 249
	i64 u0x5d418180ddc97149, ; 462: es/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 364
	i64 u0x5d6ca10d35e9485b, ; 463: lib_Xamarin.AndroidX.Concurrent.Futures.dll.so => 237
	i64 u0x5d7ec76c1c703055, ; 464: System.Threading.Tasks.Parallel => 144
	i64 u0x5db0cbbd1028510e, ; 465: lib_System.Runtime.InteropServices.dll.so => 108
	i64 u0x5db30905d3e5013b, ; 466: Xamarin.AndroidX.Collection.Jvm.dll => 235
	i64 u0x5e467bc8f09ad026, ; 467: System.Collections.Specialized.dll => 11
	i64 u0x5e5173b3208d97e7, ; 468: System.Runtime.Handles.dll => 105
	i64 u0x5ea92fdb19ec8c4c, ; 469: System.Text.Encodings.Web.dll => 137
	i64 u0x5eb8046dd40e9ac3, ; 470: System.ComponentModel.Primitives => 16
	i64 u0x5ec272d219c9aba4, ; 471: System.Security.Cryptography.Csp.dll => 122
	i64 u0x5eee1376d94c7f5e, ; 472: System.Net.HttpListener.dll => 66
	i64 u0x5f36ccf5c6a57e24, ; 473: System.Xml.ReaderWriter.dll => 157
	i64 u0x5f4294b9b63cb842, ; 474: System.Data.Common => 22
	i64 u0x5f7399e166075632, ; 475: lib_SQLitePCLRaw.lib.e_sqlite3.android.dll.so => 211
	i64 u0x5f9a2d823f664957, ; 476: lib-el-Microsoft.Maui.Controls.resources.dll.so => 380
	i64 u0x5fa6da9c3cd8142a, ; 477: lib_Xamarin.KotlinX.Serialization.Core.dll.so => 308
	i64 u0x5fac98e0b37a5b9d, ; 478: System.Runtime.CompilerServices.Unsafe.dll => 102
	i64 u0x5fed9a6eec6702f2, ; 479: ja/Microsoft.CodeAnalysis.Workspaces.resources.dll => 354
	i64 u0x609f4b7b63d802d4, ; 480: lib_Microsoft.Extensions.DependencyInjection.dll.so => 194
	i64 u0x60cd4e33d7e60134, ; 481: Xamarin.KotlinX.Coroutines.Core.Jvm => 307
	i64 u0x60e60a6d24a89414, ; 482: tr/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 372
	i64 u0x60f62d786afcf130, ; 483: System.Memory => 63
	i64 u0x61bb78c89f867353, ; 484: System.IO => 58
	i64 u0x61be8d1299194243, ; 485: Microsoft.Maui.Controls.Xaml => 203
	i64 u0x61d2cba29557038f, ; 486: de/Microsoft.Maui.Controls.resources => 379
	i64 u0x61d88f399afb2f45, ; 487: lib_System.Runtime.Loader.dll.so => 110
	i64 u0x6217a46f11608eff, ; 488: de/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 337
	i64 u0x622eef6f9e59068d, ; 489: System.Private.CoreLib => 173
	i64 u0x627f10a4113d036d, ; 490: lib-zh-Hant-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 348
	i64 u0x62c75de2b221b541, ; 491: lib-tr-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 346
	i64 u0x63d5e3aa4ef9b931, ; 492: Xamarin.KotlinX.Coroutines.Android.dll => 305
	i64 u0x63f1f6883c1e23c2, ; 493: lib_System.Collections.Immutable.dll.so => 9
	i64 u0x6400f68068c1e9f1, ; 494: Xamarin.Google.Android.Material.dll => 295
	i64 u0x64091e0d72efeda1, ; 495: es/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 364
	i64 u0x640e3b14dbd325c2, ; 496: System.Security.Cryptography.Algorithms.dll => 120
	i64 u0x64587004560099b9, ; 497: System.Reflection => 98
	i64 u0x64b1529a438a3c45, ; 498: lib_System.Runtime.Handles.dll.so => 105
	i64 u0x6565fba2cd8f235b, ; 499: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 268
	i64 u0x659c645a2136aadf, ; 500: pt-BR/Microsoft.CodeAnalysis.Workspaces.resources => 357
	i64 u0x65d8ddec9a3de89e, ; 501: ru/Microsoft.CodeAnalysis.resources => 319
	i64 u0x65ecac39144dd3cc, ; 502: Microsoft.Maui.Controls.dll => 202
	i64 u0x65ece51227bfa724, ; 503: lib_System.Runtime.Numerics.dll.so => 111
	i64 u0x661722438787b57f, ; 504: Xamarin.AndroidX.Annotation.Jvm.dll => 227
	i64 u0x6679b2337ee6b22a, ; 505: lib_System.IO.FileSystem.Primitives.dll.so => 49
	i64 u0x6692e924eade1b29, ; 506: lib_System.Console.dll.so => 20
	i64 u0x66a4e5c6a3fb0bae, ; 507: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll.so => 267
	i64 u0x66d13304ce1a3efa, ; 508: Xamarin.AndroidX.CursorAdapter => 243
	i64 u0x674303f65d8fad6f, ; 509: lib_System.Net.Quic.dll.so => 72
	i64 u0x67488fd20632118d, ; 510: lib-es-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 351
	i64 u0x674c9df133dacc2d, ; 511: it/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 366
	i64 u0x6756ca4cad62e9d6, ; 512: lib_Xamarin.AndroidX.ConstraintLayout.Core.dll.so => 239
	i64 u0x67c0802770244408, ; 513: System.Windows.dll => 155
	i64 u0x68100b69286e27cd, ; 514: lib_System.Formats.Tar.dll.so => 39
	i64 u0x68558ec653afa616, ; 515: lib-da-Microsoft.Maui.Controls.resources.dll.so => 378
	i64 u0x6872ec7a2e36b1ac, ; 516: System.Drawing.Primitives.dll => 35
	i64 u0x68bb2c417aa9b61c, ; 517: Xamarin.KotlinX.AtomicFU.dll => 303
	i64 u0x68bcc5f7a8af5422, ; 518: Microsoft.EntityFrameworkCore.Design => 187
	i64 u0x68fbbbe2eb455198, ; 519: System.Formats.Asn1 => 38
	i64 u0x69063fc0ba8e6bdd, ; 520: he/Microsoft.Maui.Controls.resources.dll => 384
	i64 u0x695bc7b274a71abf, ; 521: System.Composition.Runtime => 217
	i64 u0x699dffb2427a2d71, ; 522: SQLitePCLRaw.lib.e_sqlite3.android.dll => 211
	i64 u0x69a3e26c76f6eec4, ; 523: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 294
	i64 u0x69c43767b6624bb2, ; 524: pl/Microsoft.CodeAnalysis.CSharp.resources => 330
	i64 u0x69e7d3906e179d54, ; 525: lib-it-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 366
	i64 u0x6a2ccdb9f29a3667, ; 526: lib-pt-BR-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 370
	i64 u0x6a4d7577b2317255, ; 527: System.Runtime.InteropServices.dll => 108
	i64 u0x6abfbfb2796f4e84, ; 528: Microsoft.CodeAnalysis.CSharp => 179
	i64 u0x6ace3b74b15ee4a4, ; 529: nb/Microsoft.Maui.Controls.resources => 393
	i64 u0x6afcedb171067e2b, ; 530: System.Core.dll => 21
	i64 u0x6b8b00fad19364b6, ; 531: lib-ru-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 358
	i64 u0x6bef98e124147c24, ; 532: Xamarin.Jetbrains.Annotations => 300
	i64 u0x6c2a741a82a7c853, ; 533: lib-pt-BR-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 357
	i64 u0x6ce874bff138ce2b, ; 534: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 266
	i64 u0x6d12bfaa99c72b1f, ; 535: lib_Microsoft.Maui.Graphics.dll.so => 206
	i64 u0x6d70755158ca866e, ; 536: lib_System.ComponentModel.EventBasedAsync.dll.so => 15
	i64 u0x6d79993361e10ef2, ; 537: Microsoft.Extensions.Primitives => 201
	i64 u0x6d7eeca99577fc8b, ; 538: lib_System.Net.WebProxy.dll.so => 79
	i64 u0x6d8515b19946b6a2, ; 539: System.Net.WebProxy.dll => 79
	i64 u0x6d86d56b84c8eb71, ; 540: lib_Xamarin.AndroidX.CursorAdapter.dll.so => 243
	i64 u0x6d9bea6b3e895cf7, ; 541: Microsoft.Extensions.Primitives.dll => 201
	i64 u0x6dd9bf4083de3f6a, ; 542: Xamarin.AndroidX.DocumentFile.dll => 246
	i64 u0x6de85c8851699e79, ; 543: Microsoft.CodeAnalysis.CSharp.Workspaces.dll => 180
	i64 u0x6e25a02c3833319a, ; 544: lib_Xamarin.AndroidX.Navigation.Fragment.dll.so => 273
	i64 u0x6e2fb2ace98ab808, ; 545: zh-Hant/Microsoft.CodeAnalysis.CSharp.resources => 335
	i64 u0x6e79c6bd8627412a, ; 546: Xamarin.AndroidX.SavedState.SavedState.Ktx => 281
	i64 u0x6e838d9a2a6f6c9e, ; 547: lib_System.ValueTuple.dll.so => 152
	i64 u0x6e9965ce1095e60a, ; 548: lib_System.Core.dll.so => 21
	i64 u0x6fd2265da78b93a4, ; 549: lib_Microsoft.Maui.dll.so => 204
	i64 u0x6fdfc7de82c33008, ; 550: cs/Microsoft.Maui.Controls.resources => 377
	i64 u0x6ffc4967cc47ba57, ; 551: System.IO.FileSystem.Watcher.dll => 50
	i64 u0x701cd46a1c25a5fe, ; 552: System.IO.FileSystem.dll => 51
	i64 u0x7078c940a89ab2ee, ; 553: ja/Microsoft.CodeAnalysis.CSharp.resources => 328
	i64 u0x70c1154d9ce7bd51, ; 554: Xamarin.Kotlin.StdLib.Common.dll => 302
	i64 u0x70e99f48c05cb921, ; 555: tr/Microsoft.Maui.Controls.resources.dll => 403
	i64 u0x70fd3deda22442d2, ; 556: lib-nb-Microsoft.Maui.Controls.resources.dll.so => 393
	i64 u0x71485e7ffdb4b958, ; 557: System.Reflection.Extensions => 94
	i64 u0x7162a2fce67a945f, ; 558: lib_Xamarin.Android.Glide.Annotations.dll.so => 220
	i64 u0x719e2fe7144bef40, ; 559: lib-fr-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 352
	i64 u0x71a495ea3761dde8, ; 560: lib-it-Microsoft.Maui.Controls.resources.dll.so => 389
	i64 u0x71ad672adbe48f35, ; 561: System.ComponentModel.Primitives.dll => 16
	i64 u0x725f5a9e82a45c81, ; 562: System.Security.Cryptography.Encoding => 123
	i64 u0x72b1fb4109e08d7b, ; 563: lib-hr-Microsoft.Maui.Controls.resources.dll.so => 386
	i64 u0x72e0300099accce1, ; 564: System.Xml.XPath.XDocument => 160
	i64 u0x730bfb248998f67a, ; 565: System.IO.Compression.ZipFile => 45
	i64 u0x732b2d67b9e5c47b, ; 566: Xamarin.Google.ErrorProne.Annotations.dll => 298
	i64 u0x732d0845f6dd3b42, ; 567: NoteApp.dll => 0
	i64 u0x734b76fdc0dc05bb, ; 568: lib_GoogleGson.dll.so => 174
	i64 u0x73a6be34e822f9d1, ; 569: lib_System.Runtime.Serialization.dll.so => 116
	i64 u0x73e4ce94e2eb6ffc, ; 570: lib_System.Memory.dll.so => 63
	i64 u0x73f2645914262879, ; 571: lib_Microsoft.EntityFrameworkCore.Sqlite.dll.so => 189
	i64 u0x743a1eccf080489a, ; 572: WindowsBase.dll => 166
	i64 u0x755a91767330b3d4, ; 573: lib_Microsoft.Extensions.Configuration.dll.so => 192
	i64 u0x75c326eb821b85c4, ; 574: lib_System.ComponentModel.DataAnnotations.dll.so => 14
	i64 u0x76012e7334db86e5, ; 575: lib_Xamarin.AndroidX.SavedState.dll.so => 280
	i64 u0x769410fc0a876efc, ; 576: tr/Microsoft.CodeAnalysis.Workspaces.resources => 359
	i64 u0x76ca07b878f44da0, ; 577: System.Runtime.Numerics.dll => 111
	i64 u0x7736c8a96e51a061, ; 578: lib_Xamarin.AndroidX.Annotation.Jvm.dll.so => 227
	i64 u0x778a805e625329ef, ; 579: System.Linq.Parallel => 60
	i64 u0x779290cc2b801eb7, ; 580: Xamarin.KotlinX.AtomicFU.Jvm => 304
	i64 u0x77f8a4acc2fdc449, ; 581: System.Security.Cryptography.Cng.dll => 121
	i64 u0x780bc73597a503a9, ; 582: lib-ms-Microsoft.Maui.Controls.resources.dll.so => 392
	i64 u0x782c5d8eb99ff201, ; 583: lib_Microsoft.VisualBasic.Core.dll.so => 2
	i64 u0x783606d1e53e7a1a, ; 584: th/Microsoft.Maui.Controls.resources.dll => 402
	i64 u0x7888c8518f32343b, ; 585: tr/Microsoft.CodeAnalysis.resources.dll => 320
	i64 u0x78a45e51311409b6, ; 586: Xamarin.AndroidX.Fragment.dll => 252
	i64 u0x78ed4ab8f9d800a1, ; 587: Xamarin.AndroidX.Lifecycle.ViewModel => 266
	i64 u0x790427a555142820, ; 588: pl/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 369
	i64 u0x7996e32deaf72986, ; 589: Microsoft.CodeAnalysis.CSharp.dll => 179
	i64 u0x7a27813eff68934c, ; 590: it/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 366
	i64 u0x7a39601d6f0bb831, ; 591: lib_Xamarin.KotlinX.AtomicFU.dll.so => 303
	i64 u0x7a7e7eddf79c5d26, ; 592: lib_Xamarin.AndroidX.Lifecycle.ViewModel.dll.so => 266
	i64 u0x7a9a57d43b0845fa, ; 593: System.AppContext => 6
	i64 u0x7ad0f4f1e5d08183, ; 594: Xamarin.AndroidX.Collection.dll => 234
	i64 u0x7adb8da2ac89b647, ; 595: fi/Microsoft.Maui.Controls.resources.dll => 382
	i64 u0x7b13d9eaa944ade8, ; 596: Xamarin.AndroidX.DynamicAnimation.dll => 248
	i64 u0x7b150145c0a9058c, ; 597: Microsoft.Data.Sqlite => 184
	i64 u0x7bef86a4335c4870, ; 598: System.ComponentModel.TypeConverter => 17
	i64 u0x7c0820144cd34d6a, ; 599: sk/Microsoft.Maui.Controls.resources.dll => 400
	i64 u0x7c2a0bd1e0f988fc, ; 600: lib-de-Microsoft.Maui.Controls.resources.dll.so => 379
	i64 u0x7c41d387501568ba, ; 601: System.Net.WebClient.dll => 77
	i64 u0x7c482cd79bd24b13, ; 602: lib_Xamarin.AndroidX.ConstraintLayout.dll.so => 238
	i64 u0x7cb684ea4e7f9d67, ; 603: ja/Microsoft.CodeAnalysis.Workspaces.resources => 354
	i64 u0x7cd2ec8eaf5241cd, ; 604: System.Security.dll => 131
	i64 u0x7cf9ae50dd350622, ; 605: Xamarin.Jetbrains.Annotations.dll => 300
	i64 u0x7d649b75d580bb42, ; 606: ms/Microsoft.Maui.Controls.resources.dll => 392
	i64 u0x7d695cf648e82e31, ; 607: lib_NoteApp.dll.so => 0
	i64 u0x7d8ee2bdc8e3aad1, ; 608: System.Numerics.Vectors => 83
	i64 u0x7df5df8db8eaa6ac, ; 609: Microsoft.Extensions.Logging.Debug => 199
	i64 u0x7df7a66da1b3f2a4, ; 610: tr/Microsoft.CodeAnalysis.Workspaces.resources.dll => 359
	i64 u0x7dfc3d6d9d8d7b70, ; 611: System.Collections => 12
	i64 u0x7e2e564fa2f76c65, ; 612: lib_System.Diagnostics.Tracing.dll.so => 34
	i64 u0x7e302e110e1e1346, ; 613: lib_System.Security.Claims.dll.so => 119
	i64 u0x7e4465b3f78ad8d0, ; 614: Xamarin.KotlinX.Serialization.Core.dll => 308
	i64 u0x7e571cad5915e6c3, ; 615: lib_Xamarin.AndroidX.Lifecycle.Process.dll.so => 261
	i64 u0x7e6b1ca712437d7d, ; 616: Xamarin.AndroidX.Emoji2.ViewsHelper => 250
	i64 u0x7e8491dff6498f33, ; 617: zh-Hans/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 347
	i64 u0x7e946809d6008ef2, ; 618: lib_System.ObjectModel.dll.so => 85
	i64 u0x7ea0077fd2273d61, ; 619: Humanizer.dll => 175
	i64 u0x7ea0272c1b4a9635, ; 620: lib_Xamarin.Android.Glide.dll.so => 219
	i64 u0x7ecc13347c8fd849, ; 621: lib_System.ComponentModel.dll.so => 18
	i64 u0x7f00ddd9b9ca5a13, ; 622: Xamarin.AndroidX.ViewPager.dll => 291
	i64 u0x7f9351cd44b1273f, ; 623: Microsoft.Extensions.Configuration.Abstractions => 193
	i64 u0x7fbd557c99b3ce6f, ; 624: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.dll.so => 259
	i64 u0x8076a9a44a2ca331, ; 625: System.Net.Quic => 72
	i64 u0x8099c2f51a031e9e, ; 626: lib-de-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 350
	i64 u0x80da183a87731838, ; 627: System.Reflection.Metadata => 95
	i64 u0x80ee53ea610b3f78, ; 628: zh-Hans/Microsoft.CodeAnalysis.CSharp.resources => 334
	i64 u0x80fa55b6d1b0be99, ; 629: SQLitePCLRaw.provider.e_sqlite3 => 212
	i64 u0x812c069d5cdecc17, ; 630: System.dll => 165
	i64 u0x81381be520a60adb, ; 631: Xamarin.AndroidX.Interpolator.dll => 254
	i64 u0x81657cec2b31e8aa, ; 632: System.Net => 82
	i64 u0x81ab745f6c0f5ce6, ; 633: zh-Hant/Microsoft.Maui.Controls.resources => 408
	i64 u0x82772feb100c9738, ; 634: it/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 340
	i64 u0x8277f2be6b5ce05f, ; 635: Xamarin.AndroidX.AppCompat => 228
	i64 u0x828f06563b30bc50, ; 636: lib_Xamarin.AndroidX.CardView.dll.so => 233
	i64 u0x82920a8d9194a019, ; 637: Xamarin.KotlinX.AtomicFU.Jvm.dll => 304
	i64 u0x82b399cb01b531c4, ; 638: lib_System.Web.dll.so => 154
	i64 u0x82df8f5532a10c59, ; 639: lib_System.Drawing.dll.so => 36
	i64 u0x82f0b6e911d13535, ; 640: lib_System.Transactions.dll.so => 151
	i64 u0x82f6403342e12049, ; 641: uk/Microsoft.Maui.Controls.resources => 404
	i64 u0x83144699b312ad81, ; 642: SQLite-net.dll => 208
	i64 u0x83a12f54cac1ef63, ; 643: lib-pl-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 369
	i64 u0x83c14ba66c8e2b8c, ; 644: zh-Hans/Microsoft.Maui.Controls.resources => 407
	i64 u0x846ce984efea52c7, ; 645: System.Threading.Tasks.Parallel.dll => 144
	i64 u0x84ae73148a4557d2, ; 646: lib_System.IO.Pipes.dll.so => 56
	i64 u0x84b01102c12a9232, ; 647: System.Runtime.Serialization.Json.dll => 113
	i64 u0x84cd5cdec0f54bcc, ; 648: lib_Microsoft.EntityFrameworkCore.Relational.dll.so => 188
	i64 u0x850c5ba0b57ce8e7, ; 649: lib_Xamarin.AndroidX.Collection.dll.so => 234
	i64 u0x851d02edd334b044, ; 650: Xamarin.AndroidX.VectorDrawable => 288
	i64 u0x855713009ceaac4f, ; 651: System.Composition.TypedParts => 218
	i64 u0x85c919db62150978, ; 652: Xamarin.AndroidX.Transition.dll => 287
	i64 u0x8662aaeb94fef37f, ; 653: lib_System.Dynamic.Runtime.dll.so => 37
	i64 u0x86a909228dc7657b, ; 654: lib-zh-Hant-Microsoft.Maui.Controls.resources.dll.so => 408
	i64 u0x86b3e00c36b84509, ; 655: Microsoft.Extensions.Configuration.dll => 192
	i64 u0x86b62cb077ec4fd7, ; 656: System.Runtime.Serialization.Xml => 115
	i64 u0x8706ffb12bf3f53d, ; 657: Xamarin.AndroidX.Annotation.Experimental => 226
	i64 u0x872a5b14c18d328c, ; 658: System.ComponentModel.DataAnnotations => 14
	i64 u0x872fb9615bc2dff0, ; 659: Xamarin.Android.Glide.Annotations.dll => 220
	i64 u0x87c4b8a492b176ad, ; 660: Microsoft.EntityFrameworkCore.Abstractions => 186
	i64 u0x87c69b87d9283884, ; 661: lib_System.Threading.Thread.dll.so => 146
	i64 u0x87f6569b25707834, ; 662: System.IO.Compression.Brotli.dll => 43
	i64 u0x8842b3a5d2d3fb36, ; 663: Microsoft.Maui.Essentials => 205
	i64 u0x88826e51a5d4a3d0, ; 664: de/Microsoft.CodeAnalysis.resources.dll => 311
	i64 u0x88926583efe7ee86, ; 665: Xamarin.AndroidX.Activity.Ktx.dll => 224
	i64 u0x88ba6bc4f7762b03, ; 666: lib_System.Reflection.dll.so => 98
	i64 u0x88bda98e0cffb7a9, ; 667: lib_Xamarin.KotlinX.Coroutines.Core.Jvm.dll.so => 307
	i64 u0x8930322c7bd8f768, ; 668: netstandard => 168
	i64 u0x897a606c9e39c75f, ; 669: lib_System.ComponentModel.Primitives.dll.so => 16
	i64 u0x89911a22005b92b7, ; 670: System.IO.FileSystem.DriveInfo.dll => 48
	i64 u0x89c5188089ec2cd5, ; 671: lib_System.Runtime.InteropServices.RuntimeInformation.dll.so => 107
	i64 u0x8a19e3dc71b34b2c, ; 672: System.Reflection.TypeExtensions.dll => 97
	i64 u0x8a399a706fcbce4b, ; 673: Microsoft.Extensions.Caching.Abstractions => 190
	i64 u0x8a57a9356f6abd4a, ; 674: lib-es-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 338
	i64 u0x8ad229ea26432ee2, ; 675: Xamarin.AndroidX.Loader => 270
	i64 u0x8b4ff5d0fdd5faa1, ; 676: lib_System.Diagnostics.DiagnosticSource.dll.so => 27
	i64 u0x8b523f8a283733d8, ; 677: ru/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 345
	i64 u0x8b541d476eb3774c, ; 678: System.Security.Principal.Windows => 128
	i64 u0x8b8d01333a96d0b5, ; 679: System.Diagnostics.Process.dll => 29
	i64 u0x8b9ceca7acae3451, ; 680: lib-he-Microsoft.Maui.Controls.resources.dll.so => 384
	i64 u0x8c39b02ed181787b, ; 681: pt-BR/Microsoft.CodeAnalysis.CSharp.resources => 331
	i64 u0x8cb8f612b633affb, ; 682: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 281
	i64 u0x8cdfdb4ce85fb925, ; 683: lib_System.Security.Principal.Windows.dll.so => 128
	i64 u0x8cdfe7b8f4caa426, ; 684: System.IO.Compression.FileSystem => 44
	i64 u0x8d0f420977c2c1c7, ; 685: Xamarin.AndroidX.CursorAdapter.dll => 243
	i64 u0x8d52a25632e81824, ; 686: Microsoft.EntityFrameworkCore.Sqlite.dll => 189
	i64 u0x8d52f7ea2796c531, ; 687: Xamarin.AndroidX.Emoji2.dll => 249
	i64 u0x8d7b8ab4b3310ead, ; 688: System.Threading => 149
	i64 u0x8da188285aadfe8e, ; 689: System.Collections.Concurrent => 8
	i64 u0x8ed5e23fbc329c35, ; 690: cs/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 336
	i64 u0x8ed807bfe9858dfc, ; 691: Xamarin.AndroidX.Navigation.Common => 272
	i64 u0x8ee08b8194a30f48, ; 692: lib-hi-Microsoft.Maui.Controls.resources.dll.so => 385
	i64 u0x8ef7601039857a44, ; 693: lib-ro-Microsoft.Maui.Controls.resources.dll.so => 398
	i64 u0x8ef9414937d93a0a, ; 694: SQLitePCLRaw.core.dll => 210
	i64 u0x8f32c6f611f6ffab, ; 695: pt/Microsoft.Maui.Controls.resources.dll => 397
	i64 u0x8f44b45eb046bbd1, ; 696: System.ServiceModel.Web.dll => 132
	i64 u0x8f8829d21c8985a4, ; 697: lib-pt-BR-Microsoft.Maui.Controls.resources.dll.so => 396
	i64 u0x8f8b0f07edd7b3b6, ; 698: cs/Microsoft.CodeAnalysis.resources.dll => 310
	i64 u0x8fa404e6277d0694, ; 699: zh-Hans/Microsoft.CodeAnalysis.CSharp.resources.dll => 334
	i64 u0x8fbf5b0114c6dcef, ; 700: System.Globalization.dll => 42
	i64 u0x8fcc8c2a81f3d9e7, ; 701: Xamarin.KotlinX.Serialization.Core => 308
	i64 u0x8fd27d934d7b3a55, ; 702: SQLitePCLRaw.core => 210
	i64 u0x90263f8448b8f572, ; 703: lib_System.Diagnostics.TraceSource.dll.so => 33
	i64 u0x903101b46fb73a04, ; 704: _Microsoft.Android.Resource.Designer => 409
	i64 u0x90393bd4865292f3, ; 705: lib_System.IO.Compression.dll.so => 46
	i64 u0x905e2b8e7ae91ae6, ; 706: System.Threading.Tasks.Extensions.dll => 143
	i64 u0x90634f86c5ebe2b5, ; 707: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 267
	i64 u0x907b636704ad79ef, ; 708: lib_Microsoft.Maui.Controls.Xaml.dll.so => 203
	i64 u0x90e9efbfd68593e0, ; 709: lib_Xamarin.AndroidX.Lifecycle.LiveData.dll.so => 258
	i64 u0x90f95fc914407a17, ; 710: lib-pl-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 330
	i64 u0x91418dc638b29e68, ; 711: lib_Xamarin.AndroidX.CustomView.dll.so => 244
	i64 u0x9157bd523cd7ed36, ; 712: lib_System.Text.Json.dll.so => 138
	i64 u0x91871232ff25d47b, ; 713: cs/Microsoft.CodeAnalysis.Workspaces.resources.dll => 349
	i64 u0x91a74f07b30d37e2, ; 714: System.Linq.dll => 62
	i64 u0x91cb86ea3b17111d, ; 715: System.ServiceModel.Web => 132
	i64 u0x91fa41a87223399f, ; 716: ca/Microsoft.Maui.Controls.resources.dll => 376
	i64 u0x92054e486c0c7ea7, ; 717: System.IO.FileSystem.DriveInfo => 48
	i64 u0x92263da4b094eef5, ; 718: lib-cs-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 349
	i64 u0x926c3cf189fe2e18, ; 719: zh-Hans/Microsoft.CodeAnalysis.resources.dll => 321
	i64 u0x928614058c40c4cd, ; 720: lib_System.Xml.XPath.XDocument.dll.so => 160
	i64 u0x92b138fffca2b01e, ; 721: lib_Xamarin.AndroidX.Arch.Core.Runtime.dll.so => 231
	i64 u0x92dfc2bfc6c6a888, ; 722: Xamarin.AndroidX.Lifecycle.LiveData => 258
	i64 u0x9315bb05aa1a03d5, ; 723: de/Microsoft.CodeAnalysis.Workspaces.resources.dll => 350
	i64 u0x933da2c779423d68, ; 724: Xamarin.Android.Glide.Annotations => 220
	i64 u0x9388aad9b7ae40ce, ; 725: lib_Xamarin.AndroidX.Lifecycle.Common.dll.so => 256
	i64 u0x93ba953181e66fd2, ; 726: lib-ru-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 332
	i64 u0x93cfa73ab28d6e35, ; 727: ms/Microsoft.Maui.Controls.resources => 392
	i64 u0x941c00d21e5c0679, ; 728: lib_Xamarin.AndroidX.Transition.dll.so => 287
	i64 u0x944077d8ca3c6580, ; 729: System.IO.Compression.dll => 46
	i64 u0x948cffedc8ed7960, ; 730: System.Xml => 164
	i64 u0x94c8990839c4bdb1, ; 731: lib_Xamarin.AndroidX.Interpolator.dll.so => 254
	i64 u0x967fc325e09bfa8c, ; 732: es/Microsoft.Maui.Controls.resources => 381
	i64 u0x9686161486d34b81, ; 733: lib_Xamarin.AndroidX.ExifInterface.dll.so => 251
	i64 u0x96ae8122ac67b30e, ; 734: zh-Hant/Microsoft.CodeAnalysis.Workspaces.resources.dll => 361
	i64 u0x96f01cc18829cc2a, ; 735: System.Composition.Hosting.dll => 216
	i64 u0x9732d8dbddea3d9a, ; 736: id/Microsoft.Maui.Controls.resources => 388
	i64 u0x978be80e5210d31b, ; 737: Microsoft.Maui.Graphics.dll => 206
	i64 u0x97b8c771ea3e4220, ; 738: System.ComponentModel.dll => 18
	i64 u0x97e144c9d3c6976e, ; 739: System.Collections.Concurrent.dll => 8
	i64 u0x98270c46908e26f7, ; 740: zh-Hant/Microsoft.CodeAnalysis.CSharp.resources.dll => 335
	i64 u0x984184e3c70d4419, ; 741: GoogleGson => 174
	i64 u0x9843944103683dd3, ; 742: Xamarin.AndroidX.Core.Core.Ktx => 242
	i64 u0x98d720cc4597562c, ; 743: System.Security.Cryptography.OpenSsl => 124
	i64 u0x991d510397f92d9d, ; 744: System.Linq.Expressions => 59
	i64 u0x996ceeb8a3da3d67, ; 745: System.Threading.Overlapped.dll => 141
	i64 u0x99a00ca5270c6878, ; 746: Xamarin.AndroidX.Navigation.Runtime => 274
	i64 u0x99a891b860c3d03b, ; 747: lib-ko-Microsoft.CodeAnalysis.resources.dll.so => 316
	i64 u0x99cdc6d1f2d3a72f, ; 748: ko/Microsoft.Maui.Controls.resources.dll => 391
	i64 u0x9a01b1da98b6ee10, ; 749: Xamarin.AndroidX.Lifecycle.Runtime.dll => 262
	i64 u0x9a102e560c6efe86, ; 750: lib-pt-BR-Microsoft.CodeAnalysis.resources.dll.so => 318
	i64 u0x9a1d5006e4ce0b3a, ; 751: pl/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 343
	i64 u0x9a5ccc274fd6e6ee, ; 752: Jsr305Binding.dll => 296
	i64 u0x9ae6940b11c02876, ; 753: lib_Xamarin.AndroidX.Window.dll.so => 293
	i64 u0x9b211a749105beac, ; 754: System.Transactions.Local => 150
	i64 u0x9b5185e0237443f4, ; 755: tr/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 372
	i64 u0x9b8734714671022d, ; 756: System.Threading.Tasks.Dataflow.dll => 142
	i64 u0x9ba8c32873c681c1, ; 757: it/Microsoft.CodeAnalysis.CSharp.resources.dll => 327
	i64 u0x9bc6aea27fbf034f, ; 758: lib_Xamarin.KotlinX.Coroutines.Core.dll.so => 306
	i64 u0x9bd8cc74558ad4c7, ; 759: Xamarin.KotlinX.AtomicFU => 303
	i64 u0x9be4124ffc84e7ee, ; 760: pl/Microsoft.CodeAnalysis.resources.dll => 317
	i64 u0x9bfc637cbff3a4ec, ; 761: lib-ru-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 345
	i64 u0x9c244ac7cda32d26, ; 762: System.Security.Cryptography.X509Certificates.dll => 126
	i64 u0x9c465f280cf43733, ; 763: lib_Xamarin.KotlinX.Coroutines.Android.dll.so => 305
	i64 u0x9c69fdfa9a154b28, ; 764: tr/Microsoft.CodeAnalysis.CSharp.resources.dll => 333
	i64 u0x9c8f6872beab6408, ; 765: System.Xml.XPath.XDocument.dll => 160
	i64 u0x9ce01cf91101ae23, ; 766: System.Xml.XmlDocument => 162
	i64 u0x9d128180c81d7ce6, ; 767: Xamarin.AndroidX.CustomView.PoolingContainer => 245
	i64 u0x9d5dbcf5a48583fe, ; 768: lib_Xamarin.AndroidX.Activity.dll.so => 223
	i64 u0x9d74dee1a7725f34, ; 769: Microsoft.Extensions.Configuration.Abstractions.dll => 193
	i64 u0x9dcb570d9792d506, ; 770: lib-ru-Microsoft.CodeAnalysis.resources.dll.so => 319
	i64 u0x9e4534b6adaf6e84, ; 771: nl/Microsoft.Maui.Controls.resources => 394
	i64 u0x9e4b95dec42769f7, ; 772: System.Diagnostics.Debug.dll => 26
	i64 u0x9e5a208afd9d15a6, ; 773: it/Microsoft.CodeAnalysis.CSharp.resources => 327
	i64 u0x9eaf1efdf6f7267e, ; 774: Xamarin.AndroidX.Navigation.Common.dll => 272
	i64 u0x9ef542cf1f78c506, ; 775: Xamarin.AndroidX.Lifecycle.LiveData.Core => 259
	i64 u0x9f308fed54f8a5e4, ; 776: tr/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 346
	i64 u0x9ff78e804996ce83, ; 777: lib-ja-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 341
	i64 u0xa00832eb975f56a8, ; 778: lib_System.Net.dll.so => 82
	i64 u0xa0ad78236b7b267f, ; 779: Xamarin.AndroidX.Window => 293
	i64 u0xa0d8259f4cc284ec, ; 780: lib_System.Security.Cryptography.dll.so => 127
	i64 u0xa0e17ca50c77a225, ; 781: lib_Xamarin.Google.Crypto.Tink.Android.dll.so => 297
	i64 u0xa0ff9b3e34d92f11, ; 782: lib_System.Resources.Writer.dll.so => 101
	i64 u0xa12fbfb4da97d9f3, ; 783: System.Threading.Timer.dll => 148
	i64 u0xa1440773ee9d341e, ; 784: Xamarin.Google.Android.Material => 295
	i64 u0xa1b9d7c27f47219f, ; 785: Xamarin.AndroidX.Navigation.UI.dll => 275
	i64 u0xa2572680829d2c7c, ; 786: System.IO.Pipelines.dll => 54
	i64 u0xa26597e57ee9c7f6, ; 787: System.Xml.XmlDocument.dll => 162
	i64 u0xa28ccd83aaaed58b, ; 788: NoteApp => 0
	i64 u0xa308401900e5bed3, ; 789: lib_mscorlib.dll.so => 167
	i64 u0xa395572e7da6c99d, ; 790: lib_System.Security.dll.so => 131
	i64 u0xa3d089b150e18d27, ; 791: pt-BR/Microsoft.CodeAnalysis.resources.dll => 318
	i64 u0xa3e683f24b43af6f, ; 792: System.Dynamic.Runtime.dll => 37
	i64 u0xa4145becdee3dc4f, ; 793: Xamarin.AndroidX.VectorDrawable.Animated => 289
	i64 u0xa46aa1eaa214539b, ; 794: ko/Microsoft.Maui.Controls.resources => 391
	i64 u0xa4edc8f2ceae241a, ; 795: System.Data.Common.dll => 22
	i64 u0xa5494f40f128ce6a, ; 796: System.Runtime.Serialization.Formatters.dll => 112
	i64 u0xa54b74df83dce92b, ; 797: System.Reflection.DispatchProxy => 90
	i64 u0xa579ed010d7e5215, ; 798: Xamarin.AndroidX.DocumentFile => 246
	i64 u0xa5b7152421ed6d98, ; 799: lib_System.IO.FileSystem.Watcher.dll.so => 50
	i64 u0xa5c3844f17b822db, ; 800: lib_System.Linq.Parallel.dll.so => 60
	i64 u0xa5ce5c755bde8cb8, ; 801: lib_System.Security.Cryptography.Csp.dll.so => 122
	i64 u0xa5e599d1e0524750, ; 802: System.Numerics.Vectors.dll => 83
	i64 u0xa5f1ba49b85dd355, ; 803: System.Security.Cryptography.dll => 127
	i64 u0xa61975a5a37873ea, ; 804: lib_System.Xml.XmlSerializer.dll.so => 163
	i64 u0xa6593e21584384d2, ; 805: lib_Jsr305Binding.dll.so => 296
	i64 u0xa66cbee0130865f7, ; 806: lib_WindowsBase.dll.so => 166
	i64 u0xa67dbee13e1df9ca, ; 807: Xamarin.AndroidX.SavedState.dll => 280
	i64 u0xa684b098dd27b296, ; 808: lib_Xamarin.AndroidX.Security.SecurityCrypto.dll.so => 282
	i64 u0xa68a420042bb9b1f, ; 809: Xamarin.AndroidX.DrawerLayout.dll => 247
	i64 u0xa6d26156d1cacc7c, ; 810: Xamarin.Android.Glide.dll => 219
	i64 u0xa75386b5cb9595aa, ; 811: Xamarin.AndroidX.Lifecycle.Runtime.Android => 263
	i64 u0xa763fbb98df8d9fb, ; 812: lib_Microsoft.Win32.Primitives.dll.so => 4
	i64 u0xa78ce3745383236a, ; 813: Xamarin.AndroidX.Lifecycle.Common.Jvm => 257
	i64 u0xa7c31b56b4dc7b33, ; 814: hu/Microsoft.Maui.Controls.resources => 387
	i64 u0xa7eab29ed44b4e7a, ; 815: Mono.Android.Export => 170
	i64 u0xa8195217cbf017b7, ; 816: Microsoft.VisualBasic.Core => 2
	i64 u0xa859a95830f367ff, ; 817: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll.so => 268
	i64 u0xa8adea9b1f260c23, ; 818: lib-it-Microsoft.CodeAnalysis.resources.dll.so => 314
	i64 u0xa8b52f21e0dbe690, ; 819: System.Runtime.Serialization.dll => 116
	i64 u0xa8ee4ed7de2efaee, ; 820: Xamarin.AndroidX.Annotation.dll => 225
	i64 u0xa90e610780116128, ; 821: lib-de-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 363
	i64 u0xa95590e7c57438a4, ; 822: System.Configuration => 19
	i64 u0xaa2219c8e3449ff5, ; 823: Microsoft.Extensions.Logging.Abstractions => 198
	i64 u0xaa443ac34067eeef, ; 824: System.Private.Xml.dll => 89
	i64 u0xaa52de307ef5d1dd, ; 825: System.Net.Http => 65
	i64 u0xaa6579a240a3dc11, ; 826: zh-Hant/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 348
	i64 u0xaa9a7b0214a5cc5c, ; 827: System.Diagnostics.StackTrace.dll => 30
	i64 u0xaaaf86367285a918, ; 828: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 195
	i64 u0xaae72bd80754669a, ; 829: lib-es-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 325
	i64 u0xaaf84bb3f052a265, ; 830: el/Microsoft.Maui.Controls.resources => 380
	i64 u0xab96f4979e4d3631, ; 831: Microsoft.CodeAnalysis.Workspaces.dll => 181
	i64 u0xab9af77b5b67a0b8, ; 832: Xamarin.AndroidX.ConstraintLayout.Core => 239
	i64 u0xab9c1b2687d86b0b, ; 833: lib_System.Linq.Expressions.dll.so => 59
	i64 u0xac2af3fa195a15ce, ; 834: System.Runtime.Numerics => 111
	i64 u0xac5376a2a538dc10, ; 835: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 259
	i64 u0xac5acae88f60357e, ; 836: System.Diagnostics.Tools.dll => 32
	i64 u0xac79c7e46047ad98, ; 837: System.Security.Principal.Windows.dll => 128
	i64 u0xac98d31068e24591, ; 838: System.Xml.XDocument => 159
	i64 u0xacd46e002c3ccb97, ; 839: ro/Microsoft.Maui.Controls.resources => 398
	i64 u0xacdd9e4180d56dda, ; 840: Xamarin.AndroidX.Concurrent.Futures => 237
	i64 u0xacf42eea7ef9cd12, ; 841: System.Threading.Channels => 140
	i64 u0xad7b995624a63282, ; 842: pt-BR/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 370
	i64 u0xad7e82ed3b0f16d0, ; 843: lib_Xamarin.AndroidX.DocumentFile.dll.so => 246
	i64 u0xad89c07347f1bad6, ; 844: nl/Microsoft.Maui.Controls.resources.dll => 394
	i64 u0xadbb53caf78a79d2, ; 845: System.Web.HttpUtility => 153
	i64 u0xadc90ab061a9e6e4, ; 846: System.ComponentModel.TypeConverter.dll => 17
	i64 u0xadca1b9030b9317e, ; 847: Xamarin.AndroidX.Collection.Ktx => 236
	i64 u0xadd8eda2edf396ad, ; 848: Xamarin.Android.Glide.GifDecoder => 222
	i64 u0xadf4cf30debbeb9a, ; 849: System.Net.ServicePoint.dll => 75
	i64 u0xadf511667bef3595, ; 850: System.Net.Security => 74
	i64 u0xae0aaa94fdcfce0f, ; 851: System.ComponentModel.EventBasedAsync.dll => 15
	i64 u0xae282bcd03739de7, ; 852: Java.Interop => 169
	i64 u0xae53579c90db1107, ; 853: System.ObjectModel.dll => 85
	i64 u0xae7ea18c61eef394, ; 854: SQLite-net => 208
	i64 u0xaeafff290ccb288d, ; 855: cs/Microsoft.CodeAnalysis.CSharp.resources => 323
	i64 u0xaec7c0c7e2ed4575, ; 856: lib_Xamarin.KotlinX.AtomicFU.Jvm.dll.so => 304
	i64 u0xaf12fb8133ac3fbb, ; 857: Microsoft.EntityFrameworkCore.Sqlite => 189
	i64 u0xaf732d0b2193b8f5, ; 858: System.Security.Cryptography.OpenSsl.dll => 124
	i64 u0xafdb94dbccd9d11c, ; 859: Xamarin.AndroidX.Lifecycle.LiveData.dll => 258
	i64 u0xafe29f45095518e7, ; 860: lib_Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll.so => 269
	i64 u0xb03ae931fb25607e, ; 861: Xamarin.AndroidX.ConstraintLayout => 238
	i64 u0xb05cc42cd94c6d9d, ; 862: lib-sv-Microsoft.Maui.Controls.resources.dll.so => 401
	i64 u0xb0ac21bec8f428c5, ; 863: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll => 265
	i64 u0xb0bb43dc52ea59f9, ; 864: System.Diagnostics.Tracing.dll => 34
	i64 u0xb0c6678edfb08a6d, ; 865: lib-es-Microsoft.CodeAnalysis.resources.dll.so => 312
	i64 u0xb1dd05401aa8ee63, ; 866: System.Security.AccessControl => 118
	i64 u0xb220631954820169, ; 867: System.Text.RegularExpressions => 139
	i64 u0xb2376e1dbf8b4ed7, ; 868: System.Security.Cryptography.Csp => 122
	i64 u0xb2a1959fe95c5402, ; 869: lib_System.Runtime.InteropServices.JavaScript.dll.so => 106
	i64 u0xb2a3f67f3bf29fce, ; 870: da/Microsoft.Maui.Controls.resources => 378
	i64 u0xb2aeb4459ab4812d, ; 871: es/Microsoft.CodeAnalysis.CSharp.Workspaces.resources => 338
	i64 u0xb31c53e32a474847, ; 872: zh-Hant/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 374
	i64 u0xb3874072ee0ecf8c, ; 873: Xamarin.AndroidX.VectorDrawable.Animated.dll => 289
	i64 u0xb3d5b1cf730ea936, ; 874: pt-BR/Microsoft.CodeAnalysis.resources => 318
	i64 u0xb3f0a0fcda8d3ebc, ; 875: Xamarin.AndroidX.CardView => 233
	i64 u0xb440dc2982bd1f9e, ; 876: lib_Microsoft.CodeAnalysis.CSharp.Workspaces.dll.so => 180
	i64 u0xb46be1aa6d4fff93, ; 877: hi/Microsoft.Maui.Controls.resources => 385
	i64 u0xb477491be13109d8, ; 878: ar/Microsoft.Maui.Controls.resources => 375
	i64 u0xb4b3092fd37a579a, ; 879: ja/Microsoft.CodeAnalysis.CSharp.resources.dll => 328
	i64 u0xb4bd7015ecee9d86, ; 880: System.IO.Pipelines => 54
	i64 u0xb4c53d9749c5f226, ; 881: lib_System.IO.FileSystem.AccessControl.dll.so => 47
	i64 u0xb4ff710863453fda, ; 882: System.Diagnostics.FileVersionInfo.dll => 28
	i64 u0xb5c38bf497a4cfe2, ; 883: lib_System.Threading.Tasks.dll.so => 145
	i64 u0xb5c7fcdafbc67ee4, ; 884: Microsoft.Extensions.Logging.Abstractions.dll => 198
	i64 u0xb5ea31d5244c6626, ; 885: System.Threading.ThreadPool.dll => 147
	i64 u0xb66575354464a3ec, ; 886: Xamarin.Kotlin.StdLib.Common => 302
	i64 u0xb69c4329ac29e7f4, ; 887: cs/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 362
	i64 u0xb6daa312e893d3c4, ; 888: lib-ja-Microsoft.CodeAnalysis.resources.dll.so => 315
	i64 u0xb7212c4683a94afe, ; 889: System.Drawing.Primitives => 35
	i64 u0xb7b7753d1f319409, ; 890: sv/Microsoft.Maui.Controls.resources => 401
	i64 u0xb7e73ccf867721d2, ; 891: Mono.TextTemplating => 207
	i64 u0xb7fec5c242fbc590, ; 892: lib-fr-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 365
	i64 u0xb81a2c6e0aee50fe, ; 893: lib_System.Private.CoreLib.dll.so => 173
	i64 u0xb8b0a9b3dfbc5cb7, ; 894: Xamarin.AndroidX.Window.Extensions.Core.Core => 294
	i64 u0xb8c60af47c08d4da, ; 895: System.Net.ServicePoint => 75
	i64 u0xb8e68d20aad91196, ; 896: lib_System.Xml.XPath.dll.so => 161
	i64 u0xb9185c33a1643eed, ; 897: Microsoft.CSharp.dll => 1
	i64 u0xb9b8001adf4ed7cc, ; 898: lib_Xamarin.AndroidX.SlidingPaneLayout.dll.so => 283
	i64 u0xb9f64d3b230def68, ; 899: lib-pt-Microsoft.Maui.Controls.resources.dll.so => 397
	i64 u0xb9fc3c8a556e3691, ; 900: ja/Microsoft.Maui.Controls.resources => 390
	i64 u0xba4670aa94a2b3c6, ; 901: lib_System.Xml.XDocument.dll.so => 159
	i64 u0xba48785529705af9, ; 902: System.Collections.dll => 12
	i64 u0xba965b8c86359996, ; 903: lib_System.Windows.dll.so => 155
	i64 u0xbb286883bc35db36, ; 904: System.Transactions.dll => 151
	i64 u0xbb65706fde942ce3, ; 905: System.Net.Sockets => 76
	i64 u0xbb822a624c99bd72, ; 906: lib-zh-Hans-Microsoft.CodeAnalysis.resources.dll.so => 321
	i64 u0xbba28979413cad9e, ; 907: lib_System.Runtime.CompilerServices.VisualC.dll.so => 103
	i64 u0xbbd180354b67271a, ; 908: System.Runtime.Serialization.Formatters => 112
	i64 u0xbc0ad520c3be6d31, ; 909: ja/Microsoft.CodeAnalysis.resources => 315
	i64 u0xbc22a245dab70cb4, ; 910: lib_SQLitePCLRaw.provider.e_sqlite3.dll.so => 212
	i64 u0xbc260cdba33291a3, ; 911: Xamarin.AndroidX.Arch.Core.Common.dll => 230
	i64 u0xbd0e2c0d55246576, ; 912: System.Net.Http.dll => 65
	i64 u0xbd3fbd85b9e1cb29, ; 913: lib_System.Net.HttpListener.dll.so => 66
	i64 u0xbd437a2cdb333d0d, ; 914: Xamarin.AndroidX.ViewPager2 => 292
	i64 u0xbd4f572d2bd0a789, ; 915: System.IO.Compression.ZipFile.dll => 45
	i64 u0xbd5d0b88d3d647a5, ; 916: lib_Xamarin.AndroidX.Browser.dll.so => 232
	i64 u0xbd877b14d0b56392, ; 917: System.Runtime.Intrinsics.dll => 109
	i64 u0xbe65a49036345cf4, ; 918: lib_System.Buffers.dll.so => 7
	i64 u0xbee38d4a88835966, ; 919: Xamarin.AndroidX.AppCompat.AppCompatResources => 229
	i64 u0xbef9919db45b4ca7, ; 920: System.IO.Pipes.AccessControl => 55
	i64 u0xbefded20264dcc84, ; 921: lib_Humanizer.dll.so => 175
	i64 u0xbf0fa68611139208, ; 922: lib_Xamarin.AndroidX.Annotation.dll.so => 225
	i64 u0xbfc1e1fb3095f2b3, ; 923: lib_System.Net.Http.Json.dll.so => 64
	i64 u0xbfd57e7eba42c6c7, ; 924: de/Microsoft.CodeAnalysis.CSharp.resources.dll => 324
	i64 u0xc040a4ab55817f58, ; 925: ar/Microsoft.Maui.Controls.resources.dll => 375
	i64 u0xc07cadab29efeba0, ; 926: Xamarin.AndroidX.Core.Core.Ktx.dll => 242
	i64 u0xc0b1800a2e6bb02c, ; 927: System.Composition.AttributedModel.dll => 214
	i64 u0xc0ca6b075aeebeec, ; 928: Mono.TextTemplating.dll => 207
	i64 u0xc0d928351ab5ca77, ; 929: System.Console.dll => 20
	i64 u0xc0f5a221a9383aea, ; 930: System.Runtime.Intrinsics => 109
	i64 u0xc111030af54d7191, ; 931: System.Resources.Writer => 101
	i64 u0xc12b8b3afa48329c, ; 932: lib_System.Linq.dll.so => 62
	i64 u0xc183ca0b74453aa9, ; 933: lib_System.Threading.Tasks.Dataflow.dll.so => 142
	i64 u0xc1afcc0a4309f4e3, ; 934: ko/Microsoft.CodeAnalysis.resources.dll => 316
	i64 u0xc1c2cb7af77b8858, ; 935: Microsoft.EntityFrameworkCore => 185
	i64 u0xc1ff9ae3cdb6e1e6, ; 936: Xamarin.AndroidX.Activity.dll => 223
	i64 u0xc26c064effb1dea9, ; 937: System.Buffers.dll => 7
	i64 u0xc28c50f32f81cc73, ; 938: ja/Microsoft.Maui.Controls.resources.dll => 390
	i64 u0xc2902f6cf5452577, ; 939: lib_Mono.Android.Export.dll.so => 170
	i64 u0xc2a3bca55b573141, ; 940: System.IO.FileSystem.Watcher => 50
	i64 u0xc2bcfec99f69365e, ; 941: Xamarin.AndroidX.ViewPager2.dll => 292
	i64 u0xc30b52815b58ac2c, ; 942: lib_System.Runtime.Serialization.Xml.dll.so => 115
	i64 u0xc312870f3556d820, ; 943: Microsoft.CodeAnalysis.Workspaces.MSBuild => 183
	i64 u0xc3492f8f90f96ce4, ; 944: lib_Microsoft.Extensions.DependencyModel.dll.so => 196
	i64 u0xc36d7d89c652f455, ; 945: System.Threading.Overlapped => 141
	i64 u0xc371a7f62e38d035, ; 946: lib_Microsoft.Build.Locator.dll.so => 177
	i64 u0xc396b285e59e5493, ; 947: GoogleGson.dll => 174
	i64 u0xc3c86c1e5e12f03d, ; 948: WindowsBase => 166
	i64 u0xc3e74964279d65e6, ; 949: zh-Hans/Microsoft.CodeAnalysis.resources => 321
	i64 u0xc417a7250be7393e, ; 950: System.Composition.TypedParts.dll => 218
	i64 u0xc421b61fd853169d, ; 951: lib_System.Net.WebSockets.Client.dll.so => 80
	i64 u0xc463e077917aa21d, ; 952: System.Runtime.Serialization.Json => 113
	i64 u0xc472ce300460ccb6, ; 953: Microsoft.EntityFrameworkCore.dll => 185
	i64 u0xc4d3858ed4d08512, ; 954: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 269
	i64 u0xc4d69851fe06342f, ; 955: lib_Microsoft.Extensions.Caching.Memory.dll.so => 191
	i64 u0xc50fded0ded1418c, ; 956: lib_System.ComponentModel.TypeConverter.dll.so => 17
	i64 u0xc519125d6bc8fb11, ; 957: lib_System.Net.Requests.dll.so => 73
	i64 u0xc5293b19e4dc230e, ; 958: Xamarin.AndroidX.Navigation.Fragment => 273
	i64 u0xc5325b2fcb37446f, ; 959: lib_System.Private.Xml.dll.so => 89
	i64 u0xc5348fd88280ebea, ; 960: lib-pl-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 343
	i64 u0xc535cb9a21385d9b, ; 961: lib_Xamarin.Android.Glide.DiskLruCache.dll.so => 221
	i64 u0xc5a0f4b95a699af7, ; 962: lib_System.Private.Uri.dll.so => 87
	i64 u0xc5cdcd5b6277579e, ; 963: lib_System.Security.Cryptography.Algorithms.dll.so => 120
	i64 u0xc5ebd1ae70875a55, ; 964: lib-tr-Microsoft.CodeAnalysis.Workspaces.resources.dll.so => 359
	i64 u0xc5ec286825cb0bf4, ; 965: Xamarin.AndroidX.Tracing.Tracing => 286
	i64 u0xc6706bc8aa7fe265, ; 966: Xamarin.AndroidX.Annotation.Jvm => 227
	i64 u0xc6c5b839055b9d6e, ; 967: lib_Mono.TextTemplating.dll.so => 207
	i64 u0xc6fbcf4db7ee4235, ; 968: lib-de-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 337
	i64 u0xc7c01e7d7c93a110, ; 969: System.Text.Encoding.Extensions.dll => 135
	i64 u0xc7ce851898a4548e, ; 970: lib_System.Web.HttpUtility.dll.so => 153
	i64 u0xc809d4089d2556b2, ; 971: System.Runtime.InteropServices.JavaScript.dll => 106
	i64 u0xc858a28d9ee5a6c5, ; 972: lib_System.Collections.Specialized.dll.so => 11
	i64 u0xc8ac7c6bf1c2ec51, ; 973: System.Reflection.DispatchProxy.dll => 90
	i64 u0xc9c62c8f354ac568, ; 974: lib_System.Diagnostics.TextWriterTraceListener.dll.so => 31
	i64 u0xca32340d8d54dcd5, ; 975: Microsoft.Extensions.Caching.Memory.dll => 191
	i64 u0xca3a723e7342c5b6, ; 976: lib-tr-Microsoft.Maui.Controls.resources.dll.so => 403
	i64 u0xca5801070d9fccfb, ; 977: System.Text.Encoding => 136
	i64 u0xcab3493c70141c2d, ; 978: pl/Microsoft.Maui.Controls.resources => 395
	i64 u0xcacfddc9f7c6de76, ; 979: ro/Microsoft.Maui.Controls.resources.dll => 398
	i64 u0xcadbc92899a777f0, ; 980: Xamarin.AndroidX.Startup.StartupRuntime => 284
	i64 u0xcb45618372c47127, ; 981: Microsoft.EntityFrameworkCore.Relational => 188
	i64 u0xcba1cb79f45292b5, ; 982: Xamarin.Android.Glide.GifDecoder.dll => 222
	i64 u0xcbb5f80c7293e696, ; 983: lib_System.Globalization.Calendars.dll.so => 40
	i64 u0xcbd4fdd9cef4a294, ; 984: lib__Microsoft.Android.Resource.Designer.dll.so => 409
	i64 u0xcc15da1e07bbd994, ; 985: Xamarin.AndroidX.SlidingPaneLayout => 283
	i64 u0xcc182c3afdc374d6, ; 986: Microsoft.Bcl.AsyncInterfaces => 176
	i64 u0xcc2876b32ef2794c, ; 987: lib_System.Text.RegularExpressions.dll.so => 139
	i64 u0xcc5c3bb714c4561e, ; 988: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 307
	i64 u0xcc76886e09b88260, ; 989: Xamarin.KotlinX.Serialization.Core.Jvm.dll => 309
	i64 u0xcc9fa2923aa1c9ef, ; 990: System.Diagnostics.Contracts.dll => 25
	i64 u0xcce367a95a834654, ; 991: fr/Microsoft.CodeAnalysis.Workspaces.resources.dll => 352
	i64 u0xccf25c4b634ccd3a, ; 992: zh-Hans/Microsoft.Maui.Controls.resources.dll => 407
	i64 u0xcd10a42808629144, ; 993: System.Net.Requests => 73
	i64 u0xcdca1b920e9f53ba, ; 994: Xamarin.AndroidX.Interpolator => 254
	i64 u0xcdd0c48b6937b21c, ; 995: Xamarin.AndroidX.SwipeRefreshLayout => 285
	i64 u0xcdf9c0d4c5deebf2, ; 996: zh-Hant/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll => 374
	i64 u0xcf23d8093f3ceadf, ; 997: System.Diagnostics.DiagnosticSource.dll => 27
	i64 u0xcf5ff6b6b2c4c382, ; 998: System.Net.Mail.dll => 67
	i64 u0xcf8fc898f98b0d34, ; 999: System.Private.Xml.Linq => 88
	i64 u0xd02da9e093d0b008, ; 1000: Microsoft.CodeAnalysis.Workspaces.MSBuild.dll => 183
	i64 u0xd04b5f59ed596e31, ; 1001: System.Reflection.Metadata.dll => 95
	i64 u0xd063299fcfc0c93f, ; 1002: lib_System.Runtime.Serialization.Json.dll.so => 113
	i64 u0xd0de8a113e976700, ; 1003: System.Diagnostics.TextWriterTraceListener => 31
	i64 u0xd0fc33d5ae5d4cb8, ; 1004: System.Runtime.Extensions => 104
	i64 u0xd118cf03aa687fdf, ; 1005: cs/Microsoft.CodeAnalysis.resources => 310
	i64 u0xd1194e1d8a8de83c, ; 1006: lib_Xamarin.AndroidX.Lifecycle.Common.Jvm.dll.so => 257
	i64 u0xd12beacdfc14f696, ; 1007: System.Dynamic.Runtime => 37
	i64 u0xd198e7ce1b6a8344, ; 1008: System.Net.Quic.dll => 72
	i64 u0xd21c7a270cad6fda, ; 1009: lib_Microsoft.EntityFrameworkCore.Design.dll.so => 187
	i64 u0xd2dd98c9336159bc, ; 1010: pl/Microsoft.CodeAnalysis.Workspaces.resources.dll => 356
	i64 u0xd2f81fbcb13ba53e, ; 1011: pt-BR/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 344
	i64 u0xd3144156a3727ebe, ; 1012: Xamarin.Google.Guava.ListenableFuture => 299
	i64 u0xd333d0af9e423810, ; 1013: System.Runtime.InteropServices => 108
	i64 u0xd33a415cb4278969, ; 1014: System.Security.Cryptography.Encoding.dll => 123
	i64 u0xd3426d966bb704f5, ; 1015: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 229
	i64 u0xd3651b6fc3125825, ; 1016: System.Private.Uri.dll => 87
	i64 u0xd373685349b1fe8b, ; 1017: Microsoft.Extensions.Logging.dll => 197
	i64 u0xd3801faafafb7698, ; 1018: System.Private.DataContractSerialization.dll => 86
	i64 u0xd3e4c8d6a2d5d470, ; 1019: it/Microsoft.Maui.Controls.resources => 389
	i64 u0xd3edcc1f25459a50, ; 1020: System.Reflection.Emit => 93
	i64 u0xd42655883bb8c19f, ; 1021: Microsoft.EntityFrameworkCore.Abstractions.dll => 186
	i64 u0xd4645626dffec99d, ; 1022: lib_Microsoft.Extensions.DependencyInjection.Abstractions.dll.so => 195
	i64 u0xd4fa0abb79079ea9, ; 1023: System.Security.Principal.dll => 129
	i64 u0xd5507e11a2b2839f, ; 1024: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 269
	i64 u0xd5d04bef8478ea19, ; 1025: Xamarin.AndroidX.Tracing.Tracing.dll => 286
	i64 u0xd60815f26a12e140, ; 1026: Microsoft.Extensions.Logging.Debug.dll => 199
	i64 u0xd65b511d6c4c27c4, ; 1027: ru/Microsoft.CodeAnalysis.Workspaces.resources.dll => 358
	i64 u0xd6694f8359737e4e, ; 1028: Xamarin.AndroidX.SavedState => 280
	i64 u0xd6949e129339eae5, ; 1029: lib_Xamarin.AndroidX.Core.Core.Ktx.dll.so => 242
	i64 u0xd6d21782156bc35b, ; 1030: Xamarin.AndroidX.SwipeRefreshLayout.dll => 285
	i64 u0xd6de019f6af72435, ; 1031: Xamarin.AndroidX.ConstraintLayout.Core.dll => 239
	i64 u0xd70956d1e6deefb9, ; 1032: Jsr305Binding => 296
	i64 u0xd72329819cbbbc44, ; 1033: lib_Microsoft.Extensions.Configuration.Abstractions.dll.so => 193
	i64 u0xd72c760af136e863, ; 1034: System.Xml.XmlSerializer.dll => 163
	i64 u0xd753f071e44c2a03, ; 1035: lib_System.Security.SecureString.dll.so => 130
	i64 u0xd7b3764ada9d341d, ; 1036: lib_Microsoft.Extensions.Logging.Abstractions.dll.so => 198
	i64 u0xd7f0088bc5ad71f2, ; 1037: Xamarin.AndroidX.VersionedParcelable => 290
	i64 u0xd8113d9a7e8ad136, ; 1038: System.CodeDom => 213
	i64 u0xd8eb7c27f86b76ec, ; 1039: System.Composition.AttributedModel => 214
	i64 u0xd8fb25e28ae30a12, ; 1040: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 277
	i64 u0xd9d55047b066d4af, ; 1041: lib_System.Composition.TypedParts.dll.so => 218
	i64 u0xda1dfa4c534a9251, ; 1042: Microsoft.Extensions.DependencyInjection => 194
	i64 u0xdad05a11827959a3, ; 1043: System.Collections.NonGeneric.dll => 10
	i64 u0xdaefdfe71aa53cf9, ; 1044: System.IO.FileSystem.Primitives => 49
	i64 u0xdb5383ab5865c007, ; 1045: lib-vi-Microsoft.Maui.Controls.resources.dll.so => 405
	i64 u0xdb58816721c02a59, ; 1046: lib_System.Reflection.Emit.ILGeneration.dll.so => 91
	i64 u0xdbeda89f832aa805, ; 1047: vi/Microsoft.Maui.Controls.resources.dll => 405
	i64 u0xdbf2a779fbc3ac31, ; 1048: System.Transactions.Local.dll => 150
	i64 u0xdbf9607a441b4505, ; 1049: System.Linq => 62
	i64 u0xdbfc90157a0de9b0, ; 1050: lib_System.Text.Encoding.dll.so => 136
	i64 u0xdc75032002d1a212, ; 1051: lib_System.Transactions.Local.dll.so => 150
	i64 u0xdca8be7403f92d4f, ; 1052: lib_System.Linq.Queryable.dll.so => 61
	i64 u0xdcbf1e32b739302e, ; 1053: de/Microsoft.CodeAnalysis.resources => 311
	i64 u0xdce2c53525640bf3, ; 1054: Microsoft.Extensions.Logging => 197
	i64 u0xdd14049e4243731e, ; 1055: lib-it-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 327
	i64 u0xdd2b722d78ef5f43, ; 1056: System.Runtime.dll => 117
	i64 u0xdd67031857c72f96, ; 1057: lib_System.Text.Encodings.Web.dll.so => 137
	i64 u0xdd92e229ad292030, ; 1058: System.Numerics.dll => 84
	i64 u0xdddcdd701e911af1, ; 1059: lib_Xamarin.AndroidX.Legacy.Support.Core.Utils.dll.so => 255
	i64 u0xdde30e6b77aa6f6c, ; 1060: lib-zh-Hans-Microsoft.Maui.Controls.resources.dll.so => 407
	i64 u0xde110ae80fa7c2e2, ; 1061: System.Xml.XDocument.dll => 159
	i64 u0xde4726fcdf63a198, ; 1062: Xamarin.AndroidX.Transition => 287
	i64 u0xde572c2b2fb32f93, ; 1063: lib_System.Threading.Tasks.Extensions.dll.so => 143
	i64 u0xde8769ebda7d8647, ; 1064: hr/Microsoft.Maui.Controls.resources.dll => 386
	i64 u0xdee075f3477ef6be, ; 1065: Xamarin.AndroidX.ExifInterface.dll => 251
	i64 u0xdf4b773de8fb1540, ; 1066: System.Net.dll => 82
	i64 u0xdfa254ebb4346068, ; 1067: System.Net.Ping => 70
	i64 u0xdfe65f83043045ba, ; 1068: es/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 338
	i64 u0xe0142572c095a480, ; 1069: Xamarin.AndroidX.AppCompat.dll => 228
	i64 u0xe021eaa401792a05, ; 1070: System.Text.Encoding.dll => 136
	i64 u0xe02f89350ec78051, ; 1071: Xamarin.AndroidX.CoordinatorLayout.dll => 240
	i64 u0xe0496b9d65ef5474, ; 1072: Xamarin.Android.Glide.DiskLruCache.dll => 221
	i64 u0xe082cda43904f933, ; 1073: lib-it-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 340
	i64 u0xe0c9c0e54d9b34ce, ; 1074: it/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 340
	i64 u0xe10b760bb1462e7a, ; 1075: lib_System.Security.Cryptography.Primitives.dll.so => 125
	i64 u0xe192a588d4410686, ; 1076: lib_System.IO.Pipelines.dll.so => 54
	i64 u0xe1a08bd3fa539e0d, ; 1077: System.Runtime.Loader => 110
	i64 u0xe1a77eb8831f7741, ; 1078: System.Security.SecureString.dll => 130
	i64 u0xe1b52f9f816c70ef, ; 1079: System.Private.Xml.Linq.dll => 88
	i64 u0xe1e199c8ab02e356, ; 1080: System.Data.DataSetExtensions.dll => 23
	i64 u0xe1e852de9692e4b8, ; 1081: es/Microsoft.CodeAnalysis.CSharp.resources => 325
	i64 u0xe1ecfdb7fff86067, ; 1082: System.Net.Security.dll => 74
	i64 u0xe2252a80fe853de4, ; 1083: lib_System.Security.Principal.dll.so => 129
	i64 u0xe22fa4c9c645db62, ; 1084: System.Diagnostics.TextWriterTraceListener.dll => 31
	i64 u0xe2420585aeceb728, ; 1085: System.Net.Requests.dll => 73
	i64 u0xe26692647e6bcb62, ; 1086: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 264
	i64 u0xe29b73bc11392966, ; 1087: lib-id-Microsoft.Maui.Controls.resources.dll.so => 388
	i64 u0xe2ad448dee50fbdf, ; 1088: System.Xml.Serialization => 158
	i64 u0xe2d920f978f5d85c, ; 1089: System.Data.DataSetExtensions => 23
	i64 u0xe2e426c7714fa0bc, ; 1090: Microsoft.Win32.Primitives.dll => 4
	i64 u0xe332bacb3eb4a806, ; 1091: Mono.Android.Export.dll => 170
	i64 u0xe3811d68d4fe8463, ; 1092: pt-BR/Microsoft.Maui.Controls.resources.dll => 396
	i64 u0xe3a586956771a0ed, ; 1093: lib_SQLite-net.dll.so => 208
	i64 u0xe3b7cbae5ad66c75, ; 1094: lib_System.Security.Cryptography.Encoding.dll.so => 123
	i64 u0xe444e79b0a785818, ; 1095: fr/Microsoft.CodeAnalysis.Workspaces.resources => 352
	i64 u0xe494f7ced4ecd10a, ; 1096: hu/Microsoft.Maui.Controls.resources.dll => 387
	i64 u0xe4a9b1e40d1e8917, ; 1097: lib-fi-Microsoft.Maui.Controls.resources.dll.so => 382
	i64 u0xe4ced86af5b5007d, ; 1098: it/Microsoft.CodeAnalysis.Workspaces.resources.dll => 353
	i64 u0xe4f74a0b5bf9703f, ; 1099: System.Runtime.Serialization.Primitives => 114
	i64 u0xe51aadb833ed7eb1, ; 1100: lib_Microsoft.CodeAnalysis.CSharp.dll.so => 179
	i64 u0xe529964b351f8a52, ; 1101: pt-BR/Microsoft.CodeAnalysis.CSharp.resources.dll => 331
	i64 u0xe5434e8a119ceb69, ; 1102: lib_Mono.Android.dll.so => 172
	i64 u0xe55703b9ce5c038a, ; 1103: System.Diagnostics.Tools => 32
	i64 u0xe57013c8afc270b5, ; 1104: Microsoft.VisualBasic => 3
	i64 u0xe62913cc36bc07ec, ; 1105: System.Xml.dll => 164
	i64 u0xe7b916eaefda3b00, ; 1106: fr/Microsoft.CodeAnalysis.resources.dll => 313
	i64 u0xe7bea09c4900a191, ; 1107: Xamarin.AndroidX.VectorDrawable.dll => 288
	i64 u0xe7dd1e7ea292e8bc, ; 1108: ko/Microsoft.CodeAnalysis.resources => 316
	i64 u0xe7e03cc18dcdeb49, ; 1109: lib_System.Diagnostics.StackTrace.dll.so => 30
	i64 u0xe7e147ff99a7a380, ; 1110: lib_System.Configuration.dll.so => 19
	i64 u0xe86b0df4ba9e5db8, ; 1111: lib_Xamarin.AndroidX.Lifecycle.Runtime.Android.dll.so => 263
	i64 u0xe896622fe0902957, ; 1112: System.Reflection.Emit.dll => 93
	i64 u0xe89a2a9ef110899b, ; 1113: System.Drawing.dll => 36
	i64 u0xe8c5f8c100b5934b, ; 1114: Microsoft.Win32.Registry => 5
	i64 u0xe901486a5c561f62, ; 1115: lib_System.Composition.Runtime.dll.so => 217
	i64 u0xe912b82a211c3a0c, ; 1116: System.Composition.Convention => 215
	i64 u0xe954f97da1fa29b7, ; 1117: lib-es-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 364
	i64 u0xe957c3976986ab72, ; 1118: lib_Xamarin.AndroidX.Window.Extensions.Core.Core.dll.so => 294
	i64 u0xe98163eb702ae5c5, ; 1119: Xamarin.AndroidX.Arch.Core.Runtime => 231
	i64 u0xe994f23ba4c143e5, ; 1120: Xamarin.KotlinX.Coroutines.Android => 305
	i64 u0xe9b9c8c0458fd92a, ; 1121: System.Windows => 155
	i64 u0xe9d166d87a7f2bdb, ; 1122: lib_Xamarin.AndroidX.Startup.StartupRuntime.dll.so => 284
	i64 u0xea5a4efc2ad81d1b, ; 1123: Xamarin.Google.ErrorProne.Annotations => 298
	i64 u0xeb2313fe9d65b785, ; 1124: Xamarin.AndroidX.ConstraintLayout.dll => 238
	i64 u0xeb2a85c519c97dc0, ; 1125: lib-zh-Hans-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 373
	i64 u0xeb5295eb539fe516, ; 1126: ko/Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources => 368
	i64 u0xeb6e275e78cb8d42, ; 1127: Xamarin.AndroidX.LocalBroadcastManager.dll => 271
	i64 u0xeca8221ac42d17b7, ; 1128: zh-Hant/Microsoft.CodeAnalysis.Workspaces.resources => 361
	i64 u0xed19c616b3fcb7eb, ; 1129: Xamarin.AndroidX.VersionedParcelable.dll => 290
	i64 u0xedc4817167106c23, ; 1130: System.Net.Sockets.dll => 76
	i64 u0xedc632067fb20ff3, ; 1131: System.Memory.dll => 63
	i64 u0xedc8e4ca71a02a8b, ; 1132: Xamarin.AndroidX.Navigation.Runtime.dll => 274
	i64 u0xededd1ea2a7b3104, ; 1133: de/Microsoft.CodeAnalysis.Workspaces.resources => 350
	i64 u0xee81f5b3f1c4f83b, ; 1134: System.Threading.ThreadPool => 147
	i64 u0xeeb7ebb80150501b, ; 1135: lib_Xamarin.AndroidX.Collection.Jvm.dll.so => 235
	i64 u0xeefc635595ef57f0, ; 1136: System.Security.Cryptography.Cng => 121
	i64 u0xef03b1b5a04e9709, ; 1137: System.Text.Encoding.CodePages.dll => 134
	i64 u0xef432781d5667f61, ; 1138: Xamarin.AndroidX.Print => 276
	i64 u0xef602c523fe2e87a, ; 1139: lib_Xamarin.Google.Guava.ListenableFuture.dll.so => 299
	i64 u0xef72742e1bcca27a, ; 1140: Microsoft.Maui.Essentials.dll => 205
	i64 u0xefd1e0c4e5c9b371, ; 1141: System.Resources.ResourceManager.dll => 100
	i64 u0xefe8f8d5ed3c72ea, ; 1142: System.Formats.Tar.dll => 39
	i64 u0xefec0b7fdc57ec42, ; 1143: Xamarin.AndroidX.Activity => 223
	i64 u0xf008bcd238ede2c8, ; 1144: System.CodeDom.dll => 213
	i64 u0xf00c29406ea45e19, ; 1145: es/Microsoft.Maui.Controls.resources.dll => 381
	i64 u0xf017a06a4015fe38, ; 1146: System.Composition.Convention.dll => 215
	i64 u0xf08d1c3986e90962, ; 1147: lib-ru-Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll.so => 371
	i64 u0xf09e47b6ae914f6e, ; 1148: System.Net.NameResolution => 68
	i64 u0xf0ac2b489fed2e35, ; 1149: lib_System.Diagnostics.Debug.dll.so => 26
	i64 u0xf0bb49dadd3a1fe1, ; 1150: lib_System.Net.ServicePoint.dll.so => 75
	i64 u0xf0de2537ee19c6ca, ; 1151: lib_System.Net.WebHeaderCollection.dll.so => 78
	i64 u0xf1138779fa181c68, ; 1152: lib_Xamarin.AndroidX.Lifecycle.Runtime.dll.so => 262
	i64 u0xf11b621fc87b983f, ; 1153: Microsoft.Maui.Controls.Xaml.dll => 203
	i64 u0xf161f4f3c3b7e62c, ; 1154: System.Data => 24
	i64 u0xf16eb650d5a464bc, ; 1155: System.ValueTuple => 152
	i64 u0xf1c4b4005493d871, ; 1156: System.Formats.Asn1.dll => 38
	i64 u0xf238bd79489d3a96, ; 1157: lib-nl-Microsoft.Maui.Controls.resources.dll.so => 394
	i64 u0xf27ac96c4a2c11ce, ; 1158: lib-fr-Microsoft.CodeAnalysis.resources.dll.so => 313
	i64 u0xf2f5129629f67302, ; 1159: pt-BR/Microsoft.CodeAnalysis.Workspaces.resources.dll => 357
	i64 u0xf2feea356ba760af, ; 1160: Xamarin.AndroidX.Arch.Core.Runtime.dll => 231
	i64 u0xf300e085f8acd238, ; 1161: lib_System.ServiceProcess.dll.so => 133
	i64 u0xf34e52b26e7e059d, ; 1162: System.Runtime.CompilerServices.VisualC.dll => 103
	i64 u0xf37221fda4ef8830, ; 1163: lib_Xamarin.Google.Android.Material.dll.so => 295
	i64 u0xf3a3da005d4159f2, ; 1164: pl/Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll => 343
	i64 u0xf3ad9b8fb3eefd12, ; 1165: lib_System.IO.UnmanagedMemoryStream.dll.so => 57
	i64 u0xf3ddfe05336abf29, ; 1166: System => 165
	i64 u0xf408654b2a135055, ; 1167: System.Reflection.Emit.ILGeneration.dll => 91
	i64 u0xf4103170a1de5bd0, ; 1168: System.Linq.Queryable.dll => 61
	i64 u0xf41b241c82f75cde, ; 1169: ru/Microsoft.CodeAnalysis.CSharp.resources.dll => 332
	i64 u0xf41eebf9fb91e2a1, ; 1170: it/Microsoft.CodeAnalysis.resources.dll => 314
	i64 u0xf42d20c23173d77c, ; 1171: lib_System.ServiceModel.Web.dll.so => 132
	i64 u0xf45bb3f8ce038e01, ; 1172: es/Microsoft.CodeAnalysis.Workspaces.resources.dll => 351
	i64 u0xf4c1dd70a5496a17, ; 1173: System.IO.Compression => 46
	i64 u0xf4d8549f44ddc6a4, ; 1174: lib_System.Composition.Convention.dll.so => 215
	i64 u0xf4ecf4b9afc64781, ; 1175: System.ServiceProcess.dll => 133
	i64 u0xf4eeeaa566e9b970, ; 1176: lib_Xamarin.AndroidX.CustomView.PoolingContainer.dll.so => 245
	i64 u0xf518f63ead11fcd1, ; 1177: System.Threading.Tasks => 145
	i64 u0xf588f7d9fcfd771e, ; 1178: lib-fr-Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll.so => 339
	i64 u0xf5967aac376787d7, ; 1179: Microsoft.CodeAnalysis.dll => 178
	i64 u0xf5fc7602fe27b333, ; 1180: System.Net.WebHeaderCollection => 78
	i64 u0xf6077741019d7428, ; 1181: Xamarin.AndroidX.CoordinatorLayout => 240
	i64 u0xf61dacd80708509f, ; 1182: Microsoft.CodeAnalysis.CSharp.Workspaces => 180
	i64 u0xf6742cbf457c450b, ; 1183: Xamarin.AndroidX.Lifecycle.Runtime.Android.dll => 263
	i64 u0xf70c0a7bf8ccf5af, ; 1184: System.Web => 154
	i64 u0xf77b20923f07c667, ; 1185: de/Microsoft.Maui.Controls.resources.dll => 379
	i64 u0xf7e2cac4c45067b3, ; 1186: lib_System.Numerics.Vectors.dll.so => 83
	i64 u0xf7e74930e0e3d214, ; 1187: zh-HK/Microsoft.Maui.Controls.resources.dll => 406
	i64 u0xf84773b5c81e3cef, ; 1188: lib-uk-Microsoft.Maui.Controls.resources.dll.so => 404
	i64 u0xf8aac5ea82de1348, ; 1189: System.Linq.Queryable => 61
	i64 u0xf8b77539b362d3ba, ; 1190: lib_System.Reflection.Primitives.dll.so => 96
	i64 u0xf8e045dc345b2ea3, ; 1191: lib_Xamarin.AndroidX.RecyclerView.dll.so => 278
	i64 u0xf915dc29808193a1, ; 1192: System.Web.HttpUtility.dll => 153
	i64 u0xf96c777a2a0686f4, ; 1193: hi/Microsoft.Maui.Controls.resources.dll => 385
	i64 u0xf9be54c8bcf8ff3b, ; 1194: System.Security.AccessControl.dll => 118
	i64 u0xf9eec5bb3a6aedc6, ; 1195: Microsoft.Extensions.Options => 200
	i64 u0xf9f832cfad9554ff, ; 1196: ru/Microsoft.CodeAnalysis.Workspaces.resources => 358
	i64 u0xfa0e82300e67f913, ; 1197: lib_System.AppContext.dll.so => 6
	i64 u0xfa2fdb27e8a2c8e8, ; 1198: System.ComponentModel.EventBasedAsync => 15
	i64 u0xfa3f278f288b0e84, ; 1199: lib_System.Net.Security.dll.so => 74
	i64 u0xfa5ed7226d978949, ; 1200: lib-ar-Microsoft.Maui.Controls.resources.dll.so => 375
	i64 u0xfa645d91e9fc4cba, ; 1201: System.Threading.Thread => 146
	i64 u0xfa72c187a9b70a63, ; 1202: lib_System.Composition.Hosting.dll.so => 216
	i64 u0xfad4d2c770e827f9, ; 1203: lib_System.IO.IsolatedStorage.dll.so => 52
	i64 u0xfaee584671def13d, ; 1204: Humanizer => 175
	i64 u0xfb022853d73b7fa5, ; 1205: lib_SQLitePCLRaw.batteries_v2.dll.so => 209
	i64 u0xfb06dd2338e6f7c4, ; 1206: System.Net.Ping.dll => 70
	i64 u0xfb087abe5365e3b7, ; 1207: lib_System.Data.DataSetExtensions.dll.so => 23
	i64 u0xfb846e949baff5ea, ; 1208: System.Xml.Serialization.dll => 158
	i64 u0xfbad3e4ce4b98145, ; 1209: System.Security.Cryptography.X509Certificates => 126
	i64 u0xfbf0a31c9fc34bc4, ; 1210: lib_System.Net.Http.dll.so => 65
	i64 u0xfc61ddcf78dd1f54, ; 1211: Xamarin.AndroidX.LocalBroadcastManager => 271
	i64 u0xfc6b7527cc280b3f, ; 1212: lib_System.Runtime.Serialization.Formatters.dll.so => 112
	i64 u0xfc719aec26adf9d9, ; 1213: Xamarin.AndroidX.Navigation.Fragment.dll => 273
	i64 u0xfc82690c2fe2735c, ; 1214: Xamarin.AndroidX.Lifecycle.Process.dll => 261
	i64 u0xfc93fc307d279893, ; 1215: System.IO.Pipes.AccessControl.dll => 55
	i64 u0xfcd302092ada6328, ; 1216: System.IO.MemoryMappedFiles.dll => 53
	i64 u0xfd22f00870e40ae0, ; 1217: lib_Xamarin.AndroidX.DrawerLayout.dll.so => 247
	i64 u0xfd49b3c1a76e2748, ; 1218: System.Runtime.InteropServices.RuntimeInformation => 107
	i64 u0xfd536c702f64dc47, ; 1219: System.Text.Encoding.Extensions => 135
	i64 u0xfd583f7657b6a1cb, ; 1220: Xamarin.AndroidX.Fragment => 252
	i64 u0xfd8dd91a2c26bd5d, ; 1221: Xamarin.AndroidX.Lifecycle.Runtime => 262
	i64 u0xfda36abccf05cf5c, ; 1222: System.Net.WebSockets.Client => 80
	i64 u0xfddbe9695626a7f5, ; 1223: Xamarin.AndroidX.Lifecycle.Common => 256
	i64 u0xfeae9952cf03b8cb, ; 1224: tr/Microsoft.Maui.Controls.resources => 403
	i64 u0xfebe1950717515f9, ; 1225: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 260
	i64 u0xfec8e01187d0178c, ; 1226: lib-ja-Microsoft.CodeAnalysis.CSharp.resources.dll.so => 328
	i64 u0xff270a55858bac8d, ; 1227: System.Security.Principal => 129
	i64 u0xff9b54613e0d2cc8, ; 1228: System.Net.Http.Json => 64
	i64 u0xffdb7a971be4ec73 ; 1229: System.ValueTuple.dll => 152
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [1230 x i32] [
	i32 42, i32 306, i32 285, i32 13, i32 181, i32 182, i32 274, i32 105,
	i32 191, i32 171, i32 48, i32 228, i32 342, i32 369, i32 7, i32 212,
	i32 86, i32 368, i32 399, i32 377, i32 405, i32 248, i32 302, i32 71,
	i32 278, i32 184, i32 12, i32 204, i32 102, i32 184, i32 406, i32 156,
	i32 367, i32 19, i32 253, i32 235, i32 161, i32 182, i32 250, i32 288,
	i32 167, i32 399, i32 10, i32 199, i32 341, i32 289, i32 96, i32 245,
	i32 247, i32 13, i32 200, i32 10, i32 361, i32 127, i32 95, i32 190,
	i32 140, i32 39, i32 400, i32 309, i32 177, i32 291, i32 213, i32 367,
	i32 396, i32 172, i32 222, i32 5, i32 205, i32 67, i32 282, i32 322,
	i32 130, i32 176, i32 281, i32 249, i32 68, i32 314, i32 339, i32 311,
	i32 236, i32 66, i32 57, i32 176, i32 244, i32 178, i32 52, i32 178,
	i32 43, i32 125, i32 332, i32 67, i32 81, i32 264, i32 329, i32 158,
	i32 92, i32 99, i32 278, i32 141, i32 324, i32 151, i32 232, i32 322,
	i32 383, i32 162, i32 169, i32 384, i32 195, i32 329, i32 81, i32 236,
	i32 333, i32 4, i32 5, i32 374, i32 51, i32 101, i32 196, i32 56,
	i32 120, i32 98, i32 168, i32 118, i32 306, i32 21, i32 336, i32 387,
	i32 137, i32 363, i32 97, i32 309, i32 323, i32 77, i32 393, i32 322,
	i32 276, i32 337, i32 284, i32 119, i32 315, i32 8, i32 325, i32 165,
	i32 402, i32 70, i32 221, i32 370, i32 265, i32 279, i32 317, i32 171,
	i32 373, i32 145, i32 40, i32 360, i32 282, i32 47, i32 30, i32 275,
	i32 391, i32 144, i32 200, i32 371, i32 163, i32 28, i32 346, i32 84,
	i32 286, i32 356, i32 77, i32 43, i32 29, i32 42, i32 103, i32 117,
	i32 226, i32 45, i32 91, i32 402, i32 56, i32 148, i32 146, i32 185,
	i32 100, i32 49, i32 20, i32 241, i32 319, i32 114, i32 371, i32 183,
	i32 219, i32 383, i32 297, i32 209, i32 362, i32 342, i32 301, i32 201,
	i32 94, i32 58, i32 320, i32 388, i32 386, i32 81, i32 336, i32 297,
	i32 169, i32 26, i32 71, i32 277, i32 251, i32 356, i32 404, i32 69,
	i32 33, i32 382, i32 14, i32 139, i32 38, i32 408, i32 177, i32 237,
	i32 367, i32 395, i32 134, i32 92, i32 88, i32 372, i32 149, i32 401,
	i32 24, i32 341, i32 138, i32 57, i32 317, i32 51, i32 380, i32 29,
	i32 157, i32 34, i32 164, i32 190, i32 252, i32 52, i32 368, i32 409,
	i32 293, i32 90, i32 233, i32 35, i32 310, i32 383, i32 157, i32 365,
	i32 9, i32 381, i32 360, i32 76, i32 55, i32 363, i32 204, i32 377,
	i32 330, i32 202, i32 13, i32 292, i32 192, i32 230, i32 109, i32 353,
	i32 326, i32 181, i32 268, i32 32, i32 104, i32 84, i32 92, i32 53,
	i32 96, i32 300, i32 58, i32 9, i32 102, i32 216, i32 353, i32 344,
	i32 244, i32 68, i32 291, i32 376, i32 339, i32 125, i32 279, i32 116,
	i32 355, i32 135, i32 126, i32 106, i32 301, i32 131, i32 232, i32 299,
	i32 147, i32 156, i32 253, i32 241, i32 209, i32 248, i32 279, i32 97,
	i32 24, i32 283, i32 143, i32 344, i32 276, i32 272, i32 3, i32 312,
	i32 167, i32 229, i32 100, i32 161, i32 99, i32 25, i32 93, i32 168,
	i32 172, i32 224, i32 3, i32 395, i32 250, i32 1, i32 114, i32 301,
	i32 186, i32 253, i32 261, i32 33, i32 6, i32 196, i32 362, i32 399,
	i32 182, i32 156, i32 335, i32 397, i32 53, i32 187, i32 255, i32 351,
	i32 85, i32 290, i32 313, i32 214, i32 275, i32 360, i32 44, i32 260,
	i32 104, i32 354, i32 334, i32 324, i32 47, i32 138, i32 64, i32 188,
	i32 342, i32 270, i32 69, i32 80, i32 59, i32 89, i32 154, i32 230,
	i32 133, i32 110, i32 389, i32 373, i32 270, i32 347, i32 277, i32 171,
	i32 134, i32 323, i32 140, i32 40, i32 376, i32 345, i32 349, i32 211,
	i32 202, i32 60, i32 267, i32 79, i32 25, i32 36, i32 99, i32 264,
	i32 71, i32 22, i32 241, i32 206, i32 329, i32 400, i32 121, i32 69,
	i32 107, i32 406, i32 271, i32 119, i32 117, i32 256, i32 326, i32 257,
	i32 11, i32 2, i32 124, i32 115, i32 142, i32 41, i32 87, i32 331,
	i32 365, i32 355, i32 225, i32 210, i32 173, i32 27, i32 148, i32 348,
	i32 390, i32 194, i32 298, i32 326, i32 224, i32 1, i32 217, i32 226,
	i32 44, i32 240, i32 149, i32 255, i32 18, i32 86, i32 378, i32 41,
	i32 355, i32 312, i32 260, i32 234, i32 333, i32 265, i32 94, i32 197,
	i32 28, i32 320, i32 41, i32 347, i32 78, i32 249, i32 364, i32 237,
	i32 144, i32 108, i32 235, i32 11, i32 105, i32 137, i32 16, i32 122,
	i32 66, i32 157, i32 22, i32 211, i32 380, i32 308, i32 102, i32 354,
	i32 194, i32 307, i32 372, i32 63, i32 58, i32 203, i32 379, i32 110,
	i32 337, i32 173, i32 348, i32 346, i32 305, i32 9, i32 295, i32 364,
	i32 120, i32 98, i32 105, i32 268, i32 357, i32 319, i32 202, i32 111,
	i32 227, i32 49, i32 20, i32 267, i32 243, i32 72, i32 351, i32 366,
	i32 239, i32 155, i32 39, i32 378, i32 35, i32 303, i32 187, i32 38,
	i32 384, i32 217, i32 211, i32 294, i32 330, i32 366, i32 370, i32 108,
	i32 179, i32 393, i32 21, i32 358, i32 300, i32 357, i32 266, i32 206,
	i32 15, i32 201, i32 79, i32 79, i32 243, i32 201, i32 246, i32 180,
	i32 273, i32 335, i32 281, i32 152, i32 21, i32 204, i32 377, i32 50,
	i32 51, i32 328, i32 302, i32 403, i32 393, i32 94, i32 220, i32 352,
	i32 389, i32 16, i32 123, i32 386, i32 160, i32 45, i32 298, i32 0,
	i32 174, i32 116, i32 63, i32 189, i32 166, i32 192, i32 14, i32 280,
	i32 359, i32 111, i32 227, i32 60, i32 304, i32 121, i32 392, i32 2,
	i32 402, i32 320, i32 252, i32 266, i32 369, i32 179, i32 366, i32 303,
	i32 266, i32 6, i32 234, i32 382, i32 248, i32 184, i32 17, i32 400,
	i32 379, i32 77, i32 238, i32 354, i32 131, i32 300, i32 392, i32 0,
	i32 83, i32 199, i32 359, i32 12, i32 34, i32 119, i32 308, i32 261,
	i32 250, i32 347, i32 85, i32 175, i32 219, i32 18, i32 291, i32 193,
	i32 259, i32 72, i32 350, i32 95, i32 334, i32 212, i32 165, i32 254,
	i32 82, i32 408, i32 340, i32 228, i32 233, i32 304, i32 154, i32 36,
	i32 151, i32 404, i32 208, i32 369, i32 407, i32 144, i32 56, i32 113,
	i32 188, i32 234, i32 288, i32 218, i32 287, i32 37, i32 408, i32 192,
	i32 115, i32 226, i32 14, i32 220, i32 186, i32 146, i32 43, i32 205,
	i32 311, i32 224, i32 98, i32 307, i32 168, i32 16, i32 48, i32 107,
	i32 97, i32 190, i32 338, i32 270, i32 27, i32 345, i32 128, i32 29,
	i32 384, i32 331, i32 281, i32 128, i32 44, i32 243, i32 189, i32 249,
	i32 149, i32 8, i32 336, i32 272, i32 385, i32 398, i32 210, i32 397,
	i32 132, i32 396, i32 310, i32 334, i32 42, i32 308, i32 210, i32 33,
	i32 409, i32 46, i32 143, i32 267, i32 203, i32 258, i32 330, i32 244,
	i32 138, i32 349, i32 62, i32 132, i32 376, i32 48, i32 349, i32 321,
	i32 160, i32 231, i32 258, i32 350, i32 220, i32 256, i32 332, i32 392,
	i32 287, i32 46, i32 164, i32 254, i32 381, i32 251, i32 361, i32 216,
	i32 388, i32 206, i32 18, i32 8, i32 335, i32 174, i32 242, i32 124,
	i32 59, i32 141, i32 274, i32 316, i32 391, i32 262, i32 318, i32 343,
	i32 296, i32 293, i32 150, i32 372, i32 142, i32 327, i32 306, i32 303,
	i32 317, i32 345, i32 126, i32 305, i32 333, i32 160, i32 162, i32 245,
	i32 223, i32 193, i32 319, i32 394, i32 26, i32 327, i32 272, i32 259,
	i32 346, i32 341, i32 82, i32 293, i32 127, i32 297, i32 101, i32 148,
	i32 295, i32 275, i32 54, i32 162, i32 0, i32 167, i32 131, i32 318,
	i32 37, i32 289, i32 391, i32 22, i32 112, i32 90, i32 246, i32 50,
	i32 60, i32 122, i32 83, i32 127, i32 163, i32 296, i32 166, i32 280,
	i32 282, i32 247, i32 219, i32 263, i32 4, i32 257, i32 387, i32 170,
	i32 2, i32 268, i32 314, i32 116, i32 225, i32 363, i32 19, i32 198,
	i32 89, i32 65, i32 348, i32 30, i32 195, i32 325, i32 380, i32 181,
	i32 239, i32 59, i32 111, i32 259, i32 32, i32 128, i32 159, i32 398,
	i32 237, i32 140, i32 370, i32 246, i32 394, i32 153, i32 17, i32 236,
	i32 222, i32 75, i32 74, i32 15, i32 169, i32 85, i32 208, i32 323,
	i32 304, i32 189, i32 124, i32 258, i32 269, i32 238, i32 401, i32 265,
	i32 34, i32 312, i32 118, i32 139, i32 122, i32 106, i32 378, i32 338,
	i32 374, i32 289, i32 318, i32 233, i32 180, i32 385, i32 375, i32 328,
	i32 54, i32 47, i32 28, i32 145, i32 198, i32 147, i32 302, i32 362,
	i32 315, i32 35, i32 401, i32 207, i32 365, i32 173, i32 294, i32 75,
	i32 161, i32 1, i32 283, i32 397, i32 390, i32 159, i32 12, i32 155,
	i32 151, i32 76, i32 321, i32 103, i32 112, i32 315, i32 212, i32 230,
	i32 65, i32 66, i32 292, i32 45, i32 232, i32 109, i32 7, i32 229,
	i32 55, i32 175, i32 225, i32 64, i32 324, i32 375, i32 242, i32 214,
	i32 207, i32 20, i32 109, i32 101, i32 62, i32 142, i32 316, i32 185,
	i32 223, i32 7, i32 390, i32 170, i32 50, i32 292, i32 115, i32 183,
	i32 196, i32 141, i32 177, i32 174, i32 166, i32 321, i32 218, i32 80,
	i32 113, i32 185, i32 269, i32 191, i32 17, i32 73, i32 273, i32 89,
	i32 343, i32 221, i32 87, i32 120, i32 359, i32 286, i32 227, i32 207,
	i32 337, i32 135, i32 153, i32 106, i32 11, i32 90, i32 31, i32 191,
	i32 403, i32 136, i32 395, i32 398, i32 284, i32 188, i32 222, i32 40,
	i32 409, i32 283, i32 176, i32 139, i32 307, i32 309, i32 25, i32 352,
	i32 407, i32 73, i32 254, i32 285, i32 374, i32 27, i32 67, i32 88,
	i32 183, i32 95, i32 113, i32 31, i32 104, i32 310, i32 257, i32 37,
	i32 72, i32 187, i32 356, i32 344, i32 299, i32 108, i32 123, i32 229,
	i32 87, i32 197, i32 86, i32 389, i32 93, i32 186, i32 195, i32 129,
	i32 269, i32 286, i32 199, i32 358, i32 280, i32 242, i32 285, i32 239,
	i32 296, i32 193, i32 163, i32 130, i32 198, i32 290, i32 213, i32 214,
	i32 277, i32 218, i32 194, i32 10, i32 49, i32 405, i32 91, i32 405,
	i32 150, i32 62, i32 136, i32 150, i32 61, i32 311, i32 197, i32 327,
	i32 117, i32 137, i32 84, i32 255, i32 407, i32 159, i32 287, i32 143,
	i32 386, i32 251, i32 82, i32 70, i32 338, i32 228, i32 136, i32 240,
	i32 221, i32 340, i32 340, i32 125, i32 54, i32 110, i32 130, i32 88,
	i32 23, i32 325, i32 74, i32 129, i32 31, i32 73, i32 264, i32 388,
	i32 158, i32 23, i32 4, i32 170, i32 396, i32 208, i32 123, i32 352,
	i32 387, i32 382, i32 353, i32 114, i32 179, i32 331, i32 172, i32 32,
	i32 3, i32 164, i32 313, i32 288, i32 316, i32 30, i32 19, i32 263,
	i32 93, i32 36, i32 5, i32 217, i32 215, i32 364, i32 294, i32 231,
	i32 305, i32 155, i32 284, i32 298, i32 238, i32 373, i32 368, i32 271,
	i32 361, i32 290, i32 76, i32 63, i32 274, i32 350, i32 147, i32 235,
	i32 121, i32 134, i32 276, i32 299, i32 205, i32 100, i32 39, i32 223,
	i32 213, i32 381, i32 215, i32 371, i32 68, i32 26, i32 75, i32 78,
	i32 262, i32 203, i32 24, i32 152, i32 38, i32 394, i32 313, i32 357,
	i32 231, i32 133, i32 103, i32 295, i32 343, i32 57, i32 165, i32 91,
	i32 61, i32 332, i32 314, i32 132, i32 351, i32 46, i32 215, i32 133,
	i32 245, i32 145, i32 339, i32 178, i32 78, i32 240, i32 180, i32 263,
	i32 154, i32 379, i32 83, i32 406, i32 404, i32 61, i32 96, i32 278,
	i32 153, i32 385, i32 118, i32 200, i32 358, i32 6, i32 15, i32 74,
	i32 375, i32 146, i32 216, i32 52, i32 175, i32 209, i32 70, i32 23,
	i32 158, i32 126, i32 65, i32 271, i32 112, i32 273, i32 261, i32 55,
	i32 53, i32 247, i32 107, i32 135, i32 252, i32 262, i32 80, i32 256,
	i32 403, i32 260, i32 328, i32 129, i32 64, i32 152
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 u0x0000000000000000, ; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx @ 9abff7703206541fdb83ffa80fe2c2753ad1997b"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
