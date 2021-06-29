package com.justtouchmobile.handpointsdk

import android.annotation.SuppressLint
import android.app.Application
import android.content.ContentProvider
import android.content.Context

@SuppressLint("StaticFieldLeak")
private var _appliation: Application? = null

val application: Application?
    get() = _appliation ?: initAndGetAppCtxWithReflection()

/**
 * This methods is only run if [appCtx] is accessed while [AppCtxInitProvider] hasn't been
 * initialized. This may happen in case you're accessing it outside the default process, or in case
 * you are accessing it in a [ContentProvider] with a higher priority than [AppCtxInitProvider]
 * (900 at the time of writing this doc).
 *
 * //from https://github.com/LouisCAD/Splitties/tree/master/appctx
 */

fun initAndGetAppCtxWithReflection(): Application? {
    // Fallback, should only run once per non default process.
    val activityThread = Class.forName("android.app.ActivityThread")
    val ctx = activityThread.getDeclaredMethod("currentApplication").invoke(null) as? Context
    if (ctx is Application) {
        _appliation = ctx
        return ctx
    }
    return ctx
}