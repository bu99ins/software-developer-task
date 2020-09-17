import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";


@Component({
	selector: 'app-root',
	template: `<div style="text-align:center" class="content">
		<h1>
			Welcome to {{title}}!
		</h1>
	</div>
	<div class="file-upload">
		<label for="file-input">
			Select files
		</label>

		<input id="file-input" #MyFile type="file" (change)="handleFileInput($event.target.files)" />
		<button type="button" class="btn-large btn-submit" [disabled]="fileToUpload==null" (click)="onSubmit()">
			<i class="material-icons">save</i>
		</button>
	</div>
	`,
	styles: []
})
export class AppComponent {
	constructor(private http: HttpClient) {}

	title = 'SalesReport-SPA';
	fileToUpload: File = null;

	handleFileInput(files: FileList) {
		this.fileToUpload = files.item(0);
	}

	onSubmit() {
		const endpoint = '/api/salesrecords';
		const formData: FormData = new FormData();
		formData.append('fileKey', this.fileToUpload, this.fileToUpload.name);
		this.http
			.post(endpoint, formData) // , { headers: yourHeadersConfig })
			.subscribe(data => {
			// do something, if upload success
		}, error => {
			console.log(error);
		});
	}
}
