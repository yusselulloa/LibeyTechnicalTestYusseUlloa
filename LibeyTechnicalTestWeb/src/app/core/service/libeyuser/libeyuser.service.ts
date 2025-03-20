import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) {}
	Find(documentNumber: string): Observable<LibeyUser> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;		
		return this.http.get<LibeyUser>(uri);
	}

	GetAll(): Observable<LibeyUser[]> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;		
		return this.http.get<LibeyUser[]>(uri);
	}

	Save(user: LibeyUser): Observable<LibeyUser> {				
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.post<LibeyUser>(uri, user).pipe(
			catchError((error) => {
			  if (error.status === 409) {
				return throwError(() => new Error("El usuario ya está registrado."));
			  } else {
				return throwError(() => new Error("Ocurrió un error al registrar el usuario."));
			  }
			})
		  );
	}

	Update(user: LibeyUser): Observable<LibeyUser> {				
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.put<LibeyUser>(uri, user);
	}

	deleteUser(documentNumber: string): Observable<any> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.delete<any>(uri);
	  }

	getDocumentTypes(): Observable<{ id: number; name: string }[]> {
		const uri = `${environment.pathLibeyTechnicalTest}DocumentType`;
		return this.http.get<{ id: number; name: string }[]>(uri);
	}

	getRegions(): Observable<{ code: string; name: string }[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Region`;
		return this.http.get<{ code: string; name: string }[]>(uri);
	}
	  
	getProvinces(regionCode: string): Observable<{ code: string; name: string }[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Province/${regionCode}`;
		return this.http.get<{ code: string; name: string }[]>(uri);
	}
	  
	getUbigeos(provinceCode: string): Observable<{ code: string; name: string }[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Ubigeo/${provinceCode}`;
		return this.http.get<{ code: string; name: string }[]>(uri);
	}
}