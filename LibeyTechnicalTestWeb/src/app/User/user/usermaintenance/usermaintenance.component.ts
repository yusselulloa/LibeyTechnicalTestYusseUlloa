import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LibeyUserService } from "src/app/core/service/libeyuser/libeyuser.service";
import { LibeyUser } from "src/app/entities/libeyuser";
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  user: LibeyUser = {
    documentNumber: "",
    documentTypeId: 1,
    name: "",
    fathersLastName: "",
    mothersLastName: "",
    address: "",
    regionCode: "",
    provinceCode: "",
    ubigeoCode: "",
    phone: "",
    email: "",
    password: "",
    active: true
  };

  modificarDoc: string = "";
  documentTypes: { id: number; name: string }[] = [];
  regions: { code: string; name: string }[] = [];
  provinces: { code: string; name: string }[] = [];
  ubigeos: { code: string; name: string }[] = [];

  constructor(private libeyUserService: LibeyUserService, private route: ActivatedRoute, private router : Router) { }
  ngOnInit(): void {   
    this.loadDocumentTypes();
    this.loadRegions();

    this.route.paramMap.subscribe(params => {
      const documentNumber = params.get("documentNumber");        
      if (documentNumber) {
        this.modificarDoc = documentNumber;
        this.loadUser(documentNumber);
      }
    });    

  }

  loadUser(documentNumber: string): void {
    this.libeyUserService.Find(documentNumber).subscribe(
      (data) => { 
        this.user = data;                  
 
        if (this.user.regionCode) {
          this.loadProvinces(this.user.regionCode);
        }
        if (this.user.provinceCode) {
          this.loadUbigeos(this.user.provinceCode);
        }
      },
      (error) => { console.error("Error obteniendo usuario", error); }
    );
  }

  Submit(): void {    
    Object.keys(this.user).forEach(key => {
      if (typeof (this.user as any)[key] === "number") {
        (this.user as any)[key] = (this.user as any)[key].toString();
      }
    });


    if(this.modificarDoc){ 
      this.libeyUserService.Update(this.user).subscribe(
        (response) => {
          swal.fire({
            toast: true,
            position: 'top-end',
            icon: "success",
            title: "Se ha actualizado correctamente",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
          });
          this.goBack(); 
        },
        (error) => {      
          swal.fire({
            toast: true,
            position: 'top-end',
            icon: "error",
            title: error.message,
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
          });
        }
      );
    } else {
  
    this.libeyUserService.Save(this.user).subscribe(
      (response) => {
      
        swal.fire({
          toast: true,
          position: 'top-end',
          icon: "success",
          title: "Se ha registrado correctamente",
          showConfirmButton: false,
          timer: 3000,
          timerProgressBar: true,
        });              
        this.resetForm();
      },
      (error) => {      
        swal.fire({
          toast: true,
          position: 'top-end',
          icon: "error",
          title: error.message,
          showConfirmButton: false,
          timer: 3000,
          timerProgressBar: true,
        });
      }

    );
    }
  }

  loadDocumentTypes(): void {
    this.libeyUserService.getDocumentTypes().subscribe(
      (data) => {
        this.documentTypes = data;       
      }      
    );
  }

  loadRegions(): void {
    this.libeyUserService.getRegions().subscribe(
      (data) => { this.regions = data; }      
    );
  }

  onDepartmentChange(): void {
    this.user.provinceCode = "";
    this.user.ubigeoCode = "";
    this.provinces = [];
    this.ubigeos = [];

    if (this.user.regionCode) {
      this.loadProvinces(this.user.regionCode);
    }
  }


  loadProvinces(regionCode: string): void {
    this.libeyUserService.getProvinces(regionCode).subscribe(
      (data) => { this.provinces = data; }
    );
  }
  
  loadUbigeos(provinceCode: string): void {
    this.libeyUserService.getUbigeos(provinceCode).subscribe(
      (data) => { this.ubigeos = data; }
    );
  }


  onProvinceChange(): void {
    this.user.ubigeoCode = "";
    this.ubigeos = [];

    if (this.user.provinceCode) {
      this.loadUbigeos(this.user.provinceCode);
    }
  }

  goBack(): void {
    this.router.navigate(['/user/card']);
  }

  resetForm(): void {
    this.user = {
      documentNumber: "",
      documentTypeId: 1,
      name: "",
      fathersLastName: "",
      mothersLastName: "",
      address: "",
      regionCode: "",
      provinceCode: "",
      ubigeoCode: "",
      phone: "",
      email: "",
      password: "",
      active: true
    };
    this.provinces = [];
    this.ubigeos = [];
  }


}