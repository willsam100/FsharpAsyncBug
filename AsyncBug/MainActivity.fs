namespace AsyncBug

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open System.Diagnostics

[<Activity (Label = "AsyncBug", MainLauncher = true, Icon = "@mipmap/icon")>]
type MainActivity () =
    inherit Activity ()

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resources.Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        let button = this.FindViewById<Button>(Resources.Id.myButton)
        let label = this.FindViewById<TextView>(Resources.Id.myTextView)

        button.Click.Add (fun args -> 

            async {
                let! result = 
                    async {

                        let e = Error "Failed"
                        return e
                    }

                sprintf "%A" result |>  Debug.WriteLine 

                match result with 
                | Ok _ -> 
                    label.Text <- "Whoop"
                | Error s -> 
                    label.Text <- s
                    
            } |> Async.StartImmediate
        )
