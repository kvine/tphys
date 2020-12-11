# Makefile generated by XPJ for LINUX64
-include Makefile.custom
ProjectName = VerifyLibrary
VerifyLibrary_cppfiles   += ./Src/Behaviour.cpp
VerifyLibrary_cppfiles   += ./Src/Collision.cpp
VerifyLibrary_cppfiles   += ./Src/PhyRaycast.cpp
VerifyLibrary_cppfiles   += ./Src/ContactReportCallback.cpp
VerifyLibrary_cppfiles   += ./Src/CallbackEventMgr.cpp
VerifyLibrary_cppfiles   += ./Src/ContactModifyCallback.cpp
VerifyLibrary_cppfiles   += ./Src/physics_scene_manager.cpp
VerifyLibrary_cppfiles   += ./Src/rigidbody.cpp
VerifyLibrary_cppfiles   += ./Src/golf.cpp
VerifyLibrary_cppfiles   += ./Src/MyMutex.cpp
VerifyLibrary_cppfiles   += ./Src/IndexQue.cpp
VerifyLibrary_cppfiles   += ./Src/SimulationMgr.cpp
VerifyLibrary_cppfiles   += ./Src/VerifyMgr.cpp
VerifyLibrary_cppfiles   += ./Src/VeriAPI.cpp

VerifyLibrary_release_hpaths    := 
VerifyLibrary_release_hpaths    += ./Include
VerifyLibrary_release_hpaths    += ./PhysXInclude
VerifyLibrary_release_hpaths    += ./PhysXInclude/collision
VerifyLibrary_release_hpaths    += ./PhysXInclude/common
VerifyLibrary_release_hpaths    += ./PhysXInclude/cooking
VerifyLibrary_release_hpaths    += ./PhysXInclude/cudamanager
VerifyLibrary_release_hpaths    += ./PhysXInclude/extensions
VerifyLibrary_release_hpaths    += ./PhysXInclude/filebuf
VerifyLibrary_release_hpaths    += ./PhysXInclude/foundation
VerifyLibrary_release_hpaths    += ./PhysXInclude/geometry
VerifyLibrary_release_hpaths    += ./PhysXInclude/GeomUtils
VerifyLibrary_release_hpaths    += ./PhysXInclude/gpu
VerifyLibrary_release_hpaths    += ./PhysXInclude/pvd
VerifyLibrary_release_hpaths    += ./PhysXInclude/solver
VerifyLibrary_release_hpaths    += ./PhysXInclude/task
VerifyLibrary_release_lpaths    := 
VerifyLibrary_release_lpaths    := ./PhysXLib/LinuxLibs
VerifyLibrary_release_defines   := $(VerifyLibrary_custom_defines)
VerifyLibrary_release_defines   += VERIAPI_EXPORTS=1
VerifyLibrary_release_defines   += NDEBUG
VerifyLibrary_release_libraries := 
VerifyLibrary_release_libraries += PhysX3_x64
VerifyLibrary_release_libraries += PxPvdSDK_x64
VerifyLibrary_release_libraries += PhysX3Cooking_x64
VerifyLibrary_release_libraries += PhysX3Extensions
VerifyLibrary_release_libraries += PhysX3Common_x64
VerifyLibrary_release_libraries += PxFoundation_x64
VerifyLibrary_release_common_cflags	:= $(VerifyLibrary_custom_cflags)
VerifyLibrary_release_common_cflags    += -MMD
VerifyLibrary_release_common_cflags    += $(addprefix -D, $(VerifyLibrary_release_defines))
VerifyLibrary_release_common_cflags    += $(addprefix -I, $(VerifyLibrary_release_hpaths))
VerifyLibrary_release_common_cflags  += -m64
VerifyLibrary_release_common_cflags  += -Werror -m64 -fPIC -msse2 -mfpmath=sse -fno-exceptions -fno-rtti -fvisibility=hidden -fvisibility-inlines-hidden
VerifyLibrary_release_common_cflags  += -g -std=c++11 -Wall -Wextra -Wstrict-aliasing=2 -fdiagnostics-show-option
VerifyLibrary_release_common_cflags  += -Wno-invalid-offsetof -Wno-uninitialized -Wno-implicit-fallthrough
VerifyLibrary_release_common_cflags  += -Wno-missing-field-initializers
VerifyLibrary_release_common_cflags  += -O3 -fno-strict-aliasing
VerifyLibrary_release_cflags	:= $(VerifyLibrary_release_common_cflags)
VerifyLibrary_release_cppflags	:= $(VerifyLibrary_release_common_cflags)
VerifyLibrary_release_lflags    := $(VerifyLibrary_custom_lflags)
VerifyLibrary_release_lflags    += $(addprefix -L, $(VerifyLibrary_release_lpaths))
VerifyLibrary_release_lflags    += -Wl,--start-group $(addprefix -l, $(VerifyLibrary_release_libraries)) -Wl,--end-group
VerifyLibrary_release_lflags  += -ldl
VerifyLibrary_release_lflags  += -lrt
VerifyLibrary_release_lflags  += -m64
VerifyLibrary_release_lflags  += -Wl,-rpath /home/ubuntu/git/tphys/VerifyLibraryProjectLinux/VerifyLibrary/PhysXLib/LinuxLibs
VerifyLibrary_release_objsdir  = $(OBJS_DIR)/VerifyLibrary_release
VerifyLibrary_release_cpp_o    = $(addprefix $(VerifyLibrary_release_objsdir)/, $(subst ./, , $(subst ../, , $(patsubst %.cpp, %.cpp.o, $(VerifyLibrary_cppfiles)))))
VerifyLibrary_release_cc_o    = $(addprefix $(VerifyLibrary_release_objsdir)/, $(subst ./, , $(subst ../, , $(patsubst %.cc, %.cc.o, $(VerifyLibrary_ccfiles)))))
VerifyLibrary_release_c_o      = $(addprefix $(VerifyLibrary_release_objsdir)/, $(subst ./, , $(subst ../, , $(patsubst %.c, %.c.o, $(VerifyLibrary_cfiles)))))
VerifyLibrary_release_obj      = $(VerifyLibrary_release_cpp_o) $(VerifyLibrary_release_cc_o) $(VerifyLibrary_release_c_o)
VerifyLibrary_release_bin      := ./bin/libVerifyLibrary.so

clean_VerifyLibrary_release: 
	@$(ECHO) clean VerifyLibrary release
	@$(RMDIR) $(VerifyLibrary_release_objsdir)
	@$(RMDIR) $(VerifyLibrary_release_bin)
	@$(RMDIR) $(DEPSDIR)/VerifyLibrary/release

build_VerifyLibrary_release: postbuild_VerifyLibrary_release
postbuild_VerifyLibrary_release: mainbuild_VerifyLibrary_release
mainbuild_VerifyLibrary_release: prebuild_VerifyLibrary_release $(VerifyLibrary_release_bin)
prebuild_VerifyLibrary_release:

$(VerifyLibrary_release_bin): $(VerifyLibrary_release_obj) 
	#mkdir -p `dirname ./bin/libVerifyLibrary_x64.so`
	$(CXX) -shared $(VerifyLibrary_release_obj) $(VerifyLibrary_release_lflags) -lc -o $@ 
	#$(CXX) -shared $(VerifyLibrary_release_lflags) $(VerifyLibrary_release_obj) -lc -o $@ 
	$(ECHO) building $@ complete!

VerifyLibrary_release_DEPDIR = $(dir $(@))/$(*F)
$(VerifyLibrary_release_cpp_o): $(VerifyLibrary_release_objsdir)/%.o:
	$(ECHO) VerifyLibrary: compiling release $(filter %$(strip $(subst .cpp.o,.cpp, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cppfiles))...
	mkdir -p $(dir $(@))
	$(CXX) $(VerifyLibrary_release_cppflags) -c $(filter %$(strip $(subst .cpp.o,.cpp, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cppfiles)) -o $@
	@mkdir -p $(dir $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cpp.o,.cpp, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cppfiles))))))
	cp $(VerifyLibrary_release_DEPDIR).d $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cpp.o,.cpp, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cppfiles))))).P; \
	  sed -e 's/#.*//' -e 's/^[^:]*: *//' -e 's/ *\\$$//' \
		-e '/^$$/ d' -e 's/$$/ :/' < $(VerifyLibrary_release_DEPDIR).d >> $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cpp.o,.cpp, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cppfiles))))).P; \
	  rm -f $(VerifyLibrary_release_DEPDIR).d

$(VerifyLibrary_release_cc_o): $(VerifyLibrary_release_objsdir)/%.o:
	$(ECHO) VerifyLibrary: compiling release $(filter %$(strip $(subst .cc.o,.cc, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_ccfiles))...
	mkdir -p $(dir $(@))
	$(CXX) $(VerifyLibrary_release_cppflags) -c $(filter %$(strip $(subst .cc.o,.cc, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_ccfiles)) -o $@
	mkdir -p $(dir $(addprefix $(DEPSDIR)/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cc.o,.cc, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_ccfiles))))))
	cp $(VerifyLibrary_release_DEPDIR).d $(addprefix $(DEPSDIR)/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cc.o,.cc, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_ccfiles))))).release.P; \
	  sed -e 's/#.*//' -e 's/^[^:]*: *//' -e 's/ *\\$$//' \
		-e '/^$$/ d' -e 's/$$/ :/' < $(VerifyLibrary_release_DEPDIR).d >> $(addprefix $(DEPSDIR)/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .cc.o,.cc, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_ccfiles))))).release.P; \
	  rm -f $(VerifyLibrary_release_DEPDIR).d

$(VerifyLibrary_release_c_o): $(VerifyLibrary_release_objsdir)/%.o:
	$(ECHO) VerifyLibrary: compiling release $(filter %$(strip $(subst .c.o,.c, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cfiles))...
	mkdir -p $(dir $(@))
	$(CC) $(VerifyLibrary_release_cflags) -c $(filter %$(strip $(subst .c.o,.c, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cfiles)) -o $@ 
	@mkdir -p $(dir $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .c.o,.c, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cfiles))))))
	cp $(VerifyLibrary_release_DEPDIR).d $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .c.o,.c, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cfiles))))).P; \
	  sed -e 's/#.*//' -e 's/^[^:]*: *//' -e 's/ *\\$$//' \
		-e '/^$$/ d' -e 's/$$/ :/' < $(VerifyLibrary_release_DEPDIR).d >> $(addprefix $(DEPSDIR)/VerifyLibrary/release/, $(subst ./, , $(subst ../, , $(filter %$(strip $(subst .c.o,.c, $(subst $(VerifyLibrary_release_objsdir),, $@))), $(VerifyLibrary_cfiles))))).P; \
	  rm -f $(VerifyLibrary_release_DEPDIR).d

clean_VerifyLibrary:  clean_VerifyLibrary_release
	rm -rf $(DEPSDIR)

export VERBOSE
ifndef VERBOSE
.SILENT:
endif
