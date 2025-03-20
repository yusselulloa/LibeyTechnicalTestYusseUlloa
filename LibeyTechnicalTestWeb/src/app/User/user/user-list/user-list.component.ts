import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LibeyUserService } from "src/app/core/service/libeyuser/libeyuser.service";
import { LibeyUser } from "src/app/entities/libeyuser";
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: LibeyUser[] = [];
  filteredUsers: LibeyUser[] = [];
  searchTerm: string = "";

  constructor(private libeyUserService: LibeyUserService, private router : Router) { }

  ngOnInit(): void {
    this.cargarUsuarios();
  }

  cargarUsuarios(): void {
    this.libeyUserService.GetAll().subscribe(
      (response) => {
        this.users = response;
        this.filteredUsers = response;        
      }
    );
  }
  
  deleteUser(documentNumber: string): void {
    Swal.fire({     
      title: "¿Estás seguro?",
      text: "No podrás revertir esta acción.",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Sí, eliminar",
      cancelButtonText: "Cancelar"
    }).then((result) => {
      if (result.isConfirmed) {
        this.libeyUserService.deleteUser(documentNumber).subscribe(
          () => {            
            Swal.fire({
                      toast: true,
                      position: 'top-end',
                      icon: "success",
                      title: "Se ha eliminado correctamente",
                      showConfirmButton: false,
                      timer: 3000,
                      timerProgressBar: true,
                    }); 
            this.cargarUsuarios();
          },
          (error) => {
            Swal.fire({
                      toast: true,
                      position: 'top-end',
                      icon: "error",
                      title: error.message,
                      showConfirmButton: false,
                      timer: 3000,
                      timerProgressBar: true,
                    });            
            console.error("Error eliminando usuario", error);
          }
        );
      }
    });
  }

  editUser(user: LibeyUser): void {    
    this.router.navigate(["/user/maintenance",user.documentNumber]);
  }

  searchUser(): void {    
    if (!this.searchTerm.trim()) {      
      this.cargarUsuarios();
      return;
    }    
    this.filteredUsers = this.users.filter(user =>
      user.documentNumber.includes(this.searchTerm) ||
      user.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      user.fathersLastName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      user.mothersLastName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );    
  }

  resetSearch(): void {
    this.searchTerm = "";
    this.filteredUsers = this.users;
  }

  goBack(): void {
    this.router.navigate(['/user/card']);
  }

}
