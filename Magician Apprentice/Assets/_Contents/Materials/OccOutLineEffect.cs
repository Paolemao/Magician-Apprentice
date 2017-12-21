using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OccOutLineEffect : MonoBehaviour {

    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class OcclusionEffect : MonoBehaviour
    {
        private const string NODE = "Occlusion Camera";

        [Range(0.0f, 10.0f)]
        public float intensity = 1.0f;
        public Vector4 tiling = new Vector4(1, 1, 0, 0);
        public Texture2D occlusionMap;
        public LayerMask cullingMask;

        private Camera occlusionCamera
        {
            get
            {
                if (null == m_OcclusionCamera)
                {
                    Transform node = transform.FindChild(NODE);
                    if (null == node)
                    {
                        node = new GameObject(NODE).transform;
                        node.parent = transform;
                        node.localPosition = Vector3.zero;
                        node.localRotation = Quaternion.identity;
                        node.localScale = Vector3.one;
                    }

                    m_OcclusionCamera = node.GetComponent<Camera>();
                    if (null == m_OcclusionCamera)
                    {
                        m_OcclusionCamera = node.gameObject.AddComponent<Camera>();
                    }

                    m_OcclusionCamera.enabled = false;
                    m_OcclusionCamera.clearFlags = CameraClearFlags.SolidColor;
                    m_OcclusionCamera.backgroundColor = new Color(0, 0, 0, 0);
                    m_OcclusionCamera.renderingPath = RenderingPath.VertexLit;
                    m_OcclusionCamera.hdr = false;
                    m_OcclusionCamera.useOcclusionCulling = false;
                    m_OcclusionCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }

                return m_OcclusionCamera;
            }
        }
        private Camera m_OcclusionCamera;

        private Shader depthShader
        {
            get
            {
                if (m_DepthShader == null)
                {
                    m_DepthShader = Shader.Find("Hidden/Camera-DepthNormalTexture");
                }

                return m_DepthShader;
            }
        }
        private Shader m_DepthShader = null;

        private Material occlusionMaterial
        {
            get
            {
                if (m_OcclusionMaterial == null)
                {
                    m_OcclusionMaterial = new Material(Shader.Find("Yogi/ImageEffect/Occlusion"));
                    m_OcclusionMaterial.hideFlags = HideFlags.HideAndDontSave;
                }

                return m_OcclusionMaterial;
            }
        }
        private Material m_OcclusionMaterial = null;

        private RenderTexture depthMap;

        private void OnPreRender()
        {
            depthMap = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);

            this.GetComponent<Camera>().depthTextureMode
            = DepthTextureMode.DepthNormals;

            occlusionCamera.fieldOfView = this.GetComponent<Camera>().fieldOfView;
            occlusionCamera.orthographic = this.GetComponent<Camera>().orthographic;
            occlusionCamera.nearClipPlane = this.GetComponent<Camera>().nearClipPlane;
            occlusionCamera.farClipPlane = this.GetComponent<Camera>().farClipPlane;
            occlusionCamera.cullingMask = cullingMask;
            occlusionCamera.targetTexture = depthMap;
            occlusionCamera.RenderWithShader(depthShader, string.Empty);
        }

        private void OnPostRender()
        {
            RenderTexture.ReleaseTemporary(depthMap);
        }

        private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
        {
            occlusionMaterial.SetTexture("_DepthMap", depthMap);
            occlusionMaterial.SetTexture("_OcclusionMap", occlusionMap);
            occlusionMaterial.SetFloat("_Intensity", intensity);
            occlusionMaterial.SetVector("_Tiling", tiling);
            Graphics.Blit(sourceTexture, destTexture, occlusionMaterial);
        }

        private void OnDisable()
        {
            OnDestroy();
        }

        private void OnDestroy()
        {
            if (null != m_OcclusionCamera)
            {
                if (Application.isPlaying)
                {
                    Destroy(m_OcclusionCamera.gameObject);
                }
                else
                {
                    DestroyImmediate(m_OcclusionCamera.gameObject);
                }
            }
        }
    }
}
